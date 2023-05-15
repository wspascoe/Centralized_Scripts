﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Script_Library
{
    class MovementWithMouse
    {
        public class PlayerController : MonoBehaviour
        {
            private void Update()
            {
                if (Input.GetMouseButton(0))
                {
                    MoveToCursor();
                }
            }

            private void MoveToCursor()
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool hasHit = Physics.Raycast(ray, out hit);
                if (hasHit)
                {
                    GetComponent<Mover>().MoveTo(hit.point);
                }
            }
        }

        public class Mover : MonoBehaviour
        {
            private void Update()
            {
                UpdateAnimator();
            }

            public void MoveTo(Vector3 destination)
            {
                GetComponent<NavMeshAgent>().destination = destination;
            }

            private void UpdateAnimator()
            {
                Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
                Vector3 localVelocity = transform.InverseTransformDirection(velocity);
                float speed = localVelocity.z;
                GetComponent<Animator>().SetFloat("Speed", speed);
            }
        }
    }
}
