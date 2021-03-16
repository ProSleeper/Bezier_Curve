using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace BD2D
{
    struct ArrowLine
    {
        public Vector3 q0;
        public Vector3 q1;
    }

    public class BezierDraw_2D : MonoBehaviour
    {
        public LineRenderer bezier;
        public LineRenderer bezier2;
        public LineRenderer bezier3;

        public Transform p0;        
        public Transform p1;
        public Transform p2;

        private ArrowLine arrowLine1;
        private ArrowLine arrowLine2;

        private Vector3 drawLineStart;
        private Vector3 drawLineEnd;
        private Vector3 drawPoint;

        public Transform showCircle;

        private float POSITION_PERCENT = 0.02f;

        private Slider slider;
        private Button button;

        // Use this for initialization

        private void ChangeClick()
        {
            SceneManager.LoadScene("3D_Scene");
        }

        private void Awake()
        {
            p0.gameObject.AddComponent<MoveCube>();
            p1.gameObject.AddComponent<MoveCube>();
            p2.gameObject.AddComponent<MoveCube>();

            slider = GameObject.Find("Slider").GetComponent<Slider>();
            button = GameObject.Find("Button").GetComponent<Button>();
            button.onClick.AddListener(ChangeClick);

            slider.value = 0.5f;

        }

        void Start()
        {
        
            //라인렌더러 설정                  
                
            bezier.positionCount = 50;
            bezier2.positionCount = 3;
            bezier3.positionCount = 2;


            bezier.startColor   = Color.red;
            bezier.endColor     = Color.red;

            bezier.startWidth = 0.1f;
            bezier.endWidth = 0.1f;

            bezier2.startColor = Color.green;
            bezier2.endColor = Color.green;

            bezier2.startWidth = 0.1f;
            bezier2.endWidth = 0.1f;

            bezier3.startColor = Color.yellow;
            bezier3.endColor = Color.yellow;

            bezier3.startWidth = 0.1f;
            bezier3.endWidth = 0.1f;

        }

        void Update()
        {
            //큐브1과 큐브2 사이의 벡터a
            arrowLine1.q0 = p0.position;
            arrowLine1.q1 = p1.position - p0.position;

            //큐브2와 큐브3 사이의 벡터b
            arrowLine2.q0 = p1.position;
            arrowLine2.q1 = p2.position - p1.position;

            bezier2.SetPosition(0, p0.position);
            bezier2.SetPosition(1, p1.position);
            bezier2.SetPosition(2, p2.position);



            for (int i = 0; i < 50; i++)
            {
                //그릴 점을 구할 벡터c의 시작점
                drawLineStart = arrowLine1.q0 + arrowLine1.q1 * POSITION_PERCENT * i;

                //그릴 점을 구할 벡터c의 끝점
                drawLineEnd = arrowLine2.q0 + arrowLine2.q1 * POSITION_PERCENT * i;

                bezier3.SetPosition(0, arrowLine1.q0 + arrowLine1.q1 * slider.value);
                bezier3.SetPosition(1, arrowLine2.q0 + arrowLine2.q1 * slider.value);


                //벡터 c의 크기(길이)
                drawLineEnd = drawLineEnd - drawLineStart;

                //그려지는 점
                drawPoint = drawLineStart + drawLineEnd * POSITION_PERCENT * i;

                //선 그리기
                bezier.SetPosition(i, drawPoint);
            }
        }

    }

}

