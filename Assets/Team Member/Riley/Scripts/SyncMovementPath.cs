using System.Collections;
using System.Collections.Generic;
using RileyMcGowan;
using UnityEngine;

public class SyncMovementPath : MonoBehaviour
{
    #region Private Vars
    private MovementPath thisMovementPath;
    private int currentStep;
    #endregion
    
    #region Public Vars
    public MovementPath syncedTracks;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<MovementPath>() != null)
        {
            thisMovementPath = GetComponent<MovementPath>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
