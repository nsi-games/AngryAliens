using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AngryAliens
{
    public class Ball : MonoBehaviour
    {
        public float releaseTime = .15f;
        public float maxDragDistance = 2f;

        public GameObject nextBall;
        [Header("Components")]
        public Rigidbody2D rigid;
        public Rigidbody2D hook;

        private bool isPressed = false;

        void Update()
        {
            if (isPressed)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (Vector3.Distance(mousePos, hook.position) > maxDragDistance)
                    rigid.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance;
                else
                    rigid.position = mousePos;
            }
        }

        void OnMouseDown()
        {
            isPressed = true;
            rigid.isKinematic = true;
        }

        void OnMouseUp()
        {
            isPressed = false;
            rigid.isKinematic = false;

            StartCoroutine(Release());
        }

        IEnumerator Release()
        {
            yield return new WaitForSeconds(releaseTime);

            GetComponent<SpringJoint2D>().enabled = false;
            this.enabled = false;

            yield return new WaitForSeconds(2f);

            if (nextBall != null)
            {
                nextBall.SetActive(true);
            }
            else
            {
                // Restart Game
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

        }

    }
}