using UnityEngine;
using System.Collections;

public class BulletCollision : MonoBehaviour {
    public float growMult;
    public Color targetColor;
    public float explosionTimer;
    public bool collided;
    public float popSize;
    public float implodeSize;
    public Rigidbody rb;    
    private Vector3 newScale;
    private Vector3 shrinkScale;
    public bool Implode;
    public float shrinkMult;
    public ParticleSystem cloud;
    public float particleSize;

    public GameObject BulletExplosion;
    public float explosionTime;
    // Use this for initialization
    void Start () {
        collided = false;
        Implode = false;
        rb = GetComponent<Rigidbody>();
        name = "cloud";
        int test = -1;
        ParticleSystem[] array = gameObject.GetComponentsInChildren<ParticleSystem>();
        for(int i = 0; i < array.Length; i++)
        {
            if (array[i].name.Equals("cloud"))
            {
                test = i;
               // Debug.Log("HITTADECLOUD");
            }
        }
        if(test > -1)cloud = array[test];

    }
	
	// Update is called once per frame
	void Update () {
        checkCollision();
        
    }
    void checkCollision()
    {
        if (collided)
        {
            if (!Implode)
            {
                newScale += Vector3.one * growMult * Time.deltaTime;
                cloud.startSize += newScale.x * particleSize;
                gameObject.transform.localScale += newScale;
                shrinkScale = newScale / 2;
            }
            else
            {
                shrinkScale += Vector3.one * shrinkMult * Time.deltaTime;
                cloud.startSize -= shrinkScale.x * particleSize;
                gameObject.transform.localScale -= shrinkScale;
            }
            // lerp color
        }
        checkSize();
    }
    void checkSize()
    {
        
        if(gameObject.transform.localScale.x > implodeSize)
        {
            Implode = true;
        }
        if(Implode && gameObject.transform.localScale.x < popSize)
        {
            Destroy(gameObject);
            GameObject explosion = Instantiate(BulletExplosion, transform.position, transform.rotation) as GameObject;
            Destroy(explosion, explosionTime);
        }
    }

   
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (!collided && collisionInfo.gameObject.tag != gameObject.tag)
        {
            collided = true;
            Destroy(gameObject, explosionTimer);
            rb.isKinematic = true;
        }
    }

   

}
