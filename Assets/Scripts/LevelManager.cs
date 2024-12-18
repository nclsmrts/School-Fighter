using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace Assets.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        public static CinemachineConfiner2D currentConfiner;

        private CinemachineBrain brain;
        private CinemachineCamera cam;

        static BoxCollider2D currentSection;


        void Start()
        {
            brain = CinemachineBrain.GetActiveBrain(0);
            currentConfiner = GameObject.Find("CM").GetComponent<CinemachineConfiner2D>();

        }


        void Update()
        {

        }

        //metodo para mudar o confiner da camera
        public static void ChangeSection(string sectionName)
        {
            //procura pelo objeto que contem o nome (sectionName),
            //e pega o colisor dele para ser o novo confiner 2d
            currentSection = GameObject.Find(sectionName).GetComponent<BoxCollider2D>();

            //se o objeto for encontrado e tiver colisor 
            if (currentSection != null)
            {
                //faz com que a camera limpe o cache do confiner anterior, esquece o confiner anterior
                currentConfiner.InvalidateBoundingShapeCache();

                currentConfiner.BoundingShape2D = currentSection;

                //reposicionar o Right Limiter, alinhado ao max x do confiner (direita do confiner)
                GameObject rightLimiter = GameObject.Find("Right");

                //Vector3(x,y,z)
                rightLimiter.transform.position = new Vector3(currentConfiner.BoundingShape2D.bounds.max.x, rightLimiter.transform.position.y);


            }

        }
    }
}