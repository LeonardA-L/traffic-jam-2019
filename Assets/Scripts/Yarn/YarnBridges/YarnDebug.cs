using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace tfj
{
    public class YarnDebug : MonoBehaviour
    {
        [YarnCommand("Log")]
        public void Log(string _logMessage)
        {
            Debug.Log(_logMessage);
        }

        [YarnCommand("Error")]
        public void Error(string _errorMessage)
        {
            Debug.LogError(_errorMessage);
        }
    }
}