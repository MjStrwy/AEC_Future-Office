namespace VRTK.Examples
{

    using UnityEngine;
    using System.Collections;

    public class BetterProtonPack : MonoBehaviour
    {

        public GameObject ProtonMainFX;
        public GameObject ProtonExtraFX;
        public AudioSource beamMainAudio;
        public AudioSource beamStartAudio;
        public AudioSource beamStopAudio;

        public ParticleSystem lightningBoltParticles;
        public ParticleSystem protonBeamParticles;
        int protonBeamFlag = 0;

        public VRTK_InteractableObject linkedObject;
        //public GameObject projectile;
        /* public Transform projectileSpawnPoint;
         public float projectileSpeed = 1000f;
         public float projectileLife = 5f;*/

        protected virtual void OnEnable()
        {
            linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

            if (linkedObject != null)
            {
                linkedObject.InteractableObjectUsed += InteractableObjectUsed;
            }
        }

        protected virtual void OnDisable()
        {
            if (linkedObject != null)
            {
                linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
            }
        }

        protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
        {
            //FireProjectile();
            StartCoroutine("ProtonPackFire");

            // ProtonPackStop();

        }

       /* protected virtual void FireProjectile()
        {
            if (projectile != null && projectileSpawnPoint != null)
            {
                GameObject clonedProjectile = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
                Rigidbody projectileRigidbody = clonedProjectile.GetComponent<Rigidbody>();
                float destroyTime = 0f;
                if (projectileRigidbody != null)
                {
                    projectileRigidbody.AddForce(clonedProjectile.transform.forward * projectileSpeed);
                    destroyTime = projectileLife;
                }
                Destroy(clonedProjectile, destroyTime);
            }
        }*/


        void Start()
        {

            ProtonMainFX.SetActive(false);
            ProtonExtraFX.SetActive(false);

        }

        void Update()
        {

            // Fire proton beam when left mouse button is pressed

            if (Input.GetButtonDown("Fire1"))
            {

                StartCoroutine("ProtonPackFire");

            }

            // Stop proton beam if left mouse button is released

            if (Input.GetButtonUp("Fire1"))
            {

                ProtonPackStop();

            }

        }

        IEnumerator ProtonPackFire()
        {

            ProtonExtraFX.SetActive(true);

            beamStartAudio.Play();
            protonBeamFlag = 0;


            yield return new WaitForSeconds(0.5f);
            // Wait for laser to charge

            if (protonBeamFlag == 0)
            {
                ProtonMainFX.SetActive(true);

                lightningBoltParticles.Play();
                protonBeamParticles.Play();


                beamMainAudio.Play();
            }

        }


        void ProtonPackStop()
        {

            protonBeamFlag = 1;

            ProtonMainFX.SetActive(false);
            lightningBoltParticles.Stop();
            protonBeamParticles.Stop();

            beamMainAudio.Stop();
            beamStartAudio.Stop();
            beamStopAudio.Play();

        }
    }
}