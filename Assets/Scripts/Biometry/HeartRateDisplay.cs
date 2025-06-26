using UnityEngine;
using TMPro;
using TsSDK;
using System.Linq;
using System.Collections.Generic;

public class HeartRateDisplay : MonoBehaviour
{
    [SerializeField] TsPpgProvider ppgProvider;
    [SerializeField] TMP_Text bpmText;

    private List<float> heartRateBatch = new List<float>();
    private const int batchSize = 10;

    void Update()
    {
        if (ppgProvider != null && ppgProvider.IsRunning)
        {
            var data = ppgProvider.GetData();
            if (data != null)
            {
                // Buscar el primer nodo con BPM válido
                var validNode = data.NodesData.FirstOrDefault(n => n.isHeartrateValid);

                if (!validNode.Equals(default(ProcessedPpgNodeData)))
                {
                    heartRateBatch.Add(validNode.heartRate);

                    // Esperar hasta tener 10 valores antes de mostrar
                    if (heartRateBatch.Count == batchSize)
                    {
                        float average = heartRateBatch.Average();
                        bpmText.text = $"{average:F0}";
                        heartRateBatch.Clear(); // Reiniciar para la siguiente tanda de 5
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
}
