
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace BD3D
{
    struct ArrowLine
    {
        public Vector3 q0;
        public Vector3 q1;
    }

    public class BezierDraw_3D : MonoBehaviour
    {
        public LineRenderer bezier;
        public LineRenderer bezier2;
        public LineRenderer bezier3;
        public LineRenderer bezier4;

        public Transform p0;
        public Transform p1;
        public Transform p2;
        public Transform p3;

        private ArrowLine cubeLine1;
        private ArrowLine cubeLine2;
        private ArrowLine cubeLine3;

        private ArrowLine innerLine1;
        private ArrowLine innerLine2;

        private Vector3 drawLineStart;
        private Vector3 drawLineEnd;
        private Vector3 drawPoint;

        public Transform showCircle;

        private float POSITION_PERCENT = 0.02f;

        Vector3 inLine1;
        Vector3 inLine2;
        Vector3 inLine3;

        Vector3 drLine1;
        Vector3 drLine2;

        private Slider slider;
        private Button button;


        // Use this for initialization

        private void ChangeClick()
        {
            SceneManager.LoadScene("2D_Scene");
        }

        private void Awake()
        {
            p0.gameObject.AddComponent<MoveCube>();
            p1.gameObject.AddComponent<MoveCube>();
            p2.gameObject.AddComponent<MoveCube>();
            p3.gameObject.AddComponent<MoveCube>();

            slider = GameObject.Find("Slider").GetComponent<Slider>();
            button = GameObject.Find("Button").GetComponent<Button>();
            button.onClick.AddListener(ChangeClick);

            slider.value = 0.5f;
        }

        

        void Start()
        {

            //라인렌더러 설정           
            bezier.positionCount = 50;
            bezier2.positionCount = 4;
            bezier3.positionCount = 3;

            bezier.startColor = Color.red;
            bezier.endColor = Color.red;


            bezier.startWidth = 0.1f;
            bezier.endWidth = 0.1f;


            bezier2.startColor = Color.yellow;
            bezier2.endColor = Color.yellow;


            bezier2.startWidth = 0.1f;
            bezier2.endWidth = 0.1f;

            bezier3.startColor = Color.green;
            bezier3.endColor = Color.green;


            bezier3.startWidth = 0.1f;
            bezier3.endWidth = 0.1f;

            bezier4.startColor = Color.blue;
            bezier4.endColor = Color.blue;


            bezier4.startWidth = 0.1f;
            bezier4.endWidth = 0.1f;

        }

        void Update()
        {
            //큐브1과 큐브2 사이의 벡터a
            cubeLine1.q0 = p0.position;
            cubeLine1.q1 = p1.position - p0.position;

            //큐브2와 큐브3 사이의 벡터b
            cubeLine2.q0 = p1.position;
            cubeLine2.q1 = p2.position - p1.position;

            //큐브3과 큐브4 사이의 벡터c
            cubeLine3.q0 = p2.position;
            cubeLine3.q1 = p3.position - p2.position;

            inLine1 = cubeLine1.q0 + cubeLine1.q1 * slider.value;
            inLine2 = cubeLine2.q0 + cubeLine2.q1 * slider.value;
            inLine3 = cubeLine3.q0 + cubeLine3.q1 * slider.value;

            //벡터 ab로 만든 벡터d
            drLine1 = inLine1 + (inLine2 - inLine1) * slider.value;

            //벡터bc로 만든 벡터e
            drLine2 = inLine2 + (inLine3 - inLine2) * slider.value;


            //선그리기
            bezier2.SetPosition(0, p0.position);
            bezier2.SetPosition(1, p1.position);
            bezier2.SetPosition(2, p2.position);
            bezier2.SetPosition(3, p3.position);

            bezier3.SetPosition(0, inLine1);
            bezier3.SetPosition(1, inLine2);
            bezier3.SetPosition(2, inLine3);

            bezier4.SetPosition(0, drLine1);
            bezier4.SetPosition(1, drLine2);

            //showCircle.position = drLine1 + (drLine2 - drLine1) * 0.5f;


            for (int i = 0; i < 50; i++)
            {
                //큐브 4개로 만든 선 3개에서
                //다시 그려질 선을 만들 벡터3개를 구함
                inLine1 = cubeLine1.q0 + cubeLine1.q1 * POSITION_PERCENT * i;
                inLine2 = cubeLine2.q0 + cubeLine2.q1 * POSITION_PERCENT * i;
                inLine3 = cubeLine3.q0 + cubeLine3.q1 * POSITION_PERCENT * i;

                //위에서 구한 벡터 3개로 만든 선 2개
                drLine1 = inLine1 + (inLine2 - inLine1) * POSITION_PERCENT * i;
                drLine2 = inLine2 + (inLine3 - inLine2) * POSITION_PERCENT * i;

                //만든 선에서 그려질 좌표
                drawPoint = drLine1 + (drLine2 - drLine1) * POSITION_PERCENT * i;

                //그림
                bezier.SetPosition(i, drawPoint);

            }
        }

    }
}


