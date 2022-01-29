using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RileyMcGowan
{
    public class Counter : MonoBehaviour
    {
        #region Private Vars
        private IEnumerator currentCo;
        [SerializeField]private List<FollowPath> players;
        #endregion

        #region Core Functions
        private void Awake()
        {
            currentCo = StartTimer();
            foreach (FollowPath player in FindObjectsOfType<FollowPath>())
            {
                players.Add(player);
            }
        }

        private void OnBecameVisible()
        {
            StopCoroutine(currentCo);
        }

        private void OnBecameInvisible()
        {
            if (gameObject.activeSelf == true)
            {
                StartCoroutine(currentCo);
            }
        }
        #endregion
        
        //#region //Intended for customisation outside myself
        private void FinishGame(GameObject winningPlayer, GameObject losingPlayer)
        {
            //TODO Finish State Code Here
            Debug.Log(winningPlayer + " wins!");
            
        }
        //#endregion

        #region Coroutines
        IEnumerator StartTimer()
        {
            while (true)
            {
                yield return new WaitForSeconds(5);
                GameObject losePlayer = gameObject.GetComponent<FollowPath>().gameObject;
                players.Remove(gameObject.GetComponent<FollowPath>());
                FinishGame(players[0].gameObject, losePlayer);
            }
        }
        #endregion
    }
}