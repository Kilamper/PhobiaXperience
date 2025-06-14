using UnityEngine;
using TMPro;
using TsSDK;
using System.Linq;

public class HeartRateDisplay : MonoBehaviour
{
    [SerializeField] TsPpgProvider ppgProvider;
    [SerializeField] TMP_Text bpmText;

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
                    bpmText.text = $"{validNode.heartRate:F0}";
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
