using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RileyMcGowan
{
    public class Counter : MonoBehaviour
    {
        private IEnumerator currentCo;
        [SerializeField]private List<FollowPath> players;

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

        private void FinishGame(GameObject winningPlayer)
        {
            Debug.Log(winningPlayer + " wins!");
        }

        IEnumerator StartTimer()
        {
            while (true)
            {
                yield return new WaitForSeconds(5);
                players.Remove(gameObject.GetComponent<FollowPath>());
                FinishGame(players[0].gameObject);
            }
        }
    }
}