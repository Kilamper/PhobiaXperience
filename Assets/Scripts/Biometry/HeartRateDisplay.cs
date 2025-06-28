using UnityEngine;
using TMPro;
using TsSDK;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class HeartRateDisplay : MonoBehaviour
{
    [SerializeField] TsPpgProvider ppgProvider;
    [SerializeField] TMP_Text bpmText;

    private List<float> heartRateBatch = new List<float>();
    private const int batchSize = 10;

    private float updateInterval = 1f; // segundos
    private bool isUpdating = false;

    void Update()
    {
        if (ppgProvider != null && ppgProvider.IsRunning)
        {
            var data = ppgProvider.GetData();
            if (data != null)
            {
                var validNode = data.NodesData.FirstOrDefault(n => n.isHeartrateValid);

                if (!validNode.Equals(default(ProcessedPpgNodeData)))
                {
                    if (!isUpdating)
                    {
                        heartRateBatch.Add(validNode.heartRate);
                        if (heartRateBatch.Count > batchSize)
                            heartRateBatch.RemoveAt(0);

                        // Solo iniciar la corrutina si no está ya corriendo
                        if (heartRateBatch.Count == batchSize)
                        {
                            StartCoroutine(UpdateBpmRoutine());
                        }
                    }
                }
                else
                {
                    bpmText.text = "---";
                }
            }
            else
            {
                bpmText.text = "ERROR";
            }
        }
        else
        {
            bpmText.text = "---";
        }
    }

    private IEnumerator UpdateBpmRoutine()
    {
        isUpdating = true;

        // Calcular y mostrar promedio
        float average = heartRateBatch.Average();
        bpmText.text = $"{average:F0}";

        // Esperar antes de permitir otra actualización
        yield return new WaitForSeconds(updateInterval);

        isUpdating = false;
    }
}
