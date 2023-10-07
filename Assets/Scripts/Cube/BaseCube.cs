using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.CubeNS {
    public class BaseCube :MonoBehaviour {
        protected Rigidbody rigidbody;
        [SerializeField] protected CubeView cubeView;

        public int currNum;
        public int currIntOfArr;

        public int maxRandomStartInt = 2;
        [Space]
        [Header("Move")]
        private float horizontalSpeed = 1300;
        private float verticalSpeed = 300;

        private void Awake() {
            rigidbody = GetComponent<Rigidbody>();
        }
        public virtual void Init() {
           // FoundManager();
            cubeView.Init();

            GenerateNum();
            SetNewParam();
        }


        protected void GenerateNum() =>
            currIntOfArr = Random.Range(0, maxRandomStartInt);

        public void SetNewParam() {
            cubeView.SetNewParam(currIntOfArr);
            currNum = (int)Mathf.Pow(2, currIntOfArr + 1);
        }

        public void MoveForward() => Move(Vector3.forward, horizontalSpeed);
        public void MoveToSide(Vector3 vector) => rigidbody.velocity = vector;
        public Vector3 Position {
            get { return transform.position; }
        }
        protected void MoveUp() => Move(Vector3.up, verticalSpeed);
        protected void Move(Vector3 vector, float speed) => rigidbody.AddForce(vector * speed);

        protected virtual void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.TryGetComponent<Cube>(out Cube otherCube)) {
                if (otherCube.currIntOfArr == currIntOfArr) {
                    rigidbody.constraints = RigidbodyConstraints.None;
                    SetValueToManagerList();
                    MoveUp();
                }
            }
        }

        protected virtual void SetValueToManagerList() { }
    }
}