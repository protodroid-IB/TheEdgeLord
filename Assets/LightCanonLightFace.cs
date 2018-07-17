using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCanonLightFace : MonoBehaviour
{
    [SerializeField]
    private LightCanonColor lightCanonColor;

    private GameObject hitFace;

    private GameObject bullet = null;

    [SerializeField]
    private Material normalMat;

    private MeshRenderer[] facesMR;
    private MeshRenderer[] beamsMR;

    private bool beamActive = false;

	// Use this for initialization
	void Start ()
    {
        facesMR = new MeshRenderer[transform.childCount];
        beamsMR = new MeshRenderer[transform.childCount];

        for(int i=0; i < facesMR.Length; i++)
        {
            facesMR[i] = transform.GetChild(i).GetComponent<MeshRenderer>();
            facesMR[i].material = normalMat;

            beamsMR[i] = facesMR[i].transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>();
            beamsMR[i].material = normalMat;
        }
	}
	
	// Update is called once per frame
	void Update () {
	}


    public void HitFace(GameObject inFace, GameObject inBullet)
    { 
        if(bullet != inBullet)
        {
            beamActive = true;
            hitFace = inFace;
            bullet = inBullet;
            Debug.Log(hitFace);
        }
    }



    public void ActivateFaceAndBeam(Material inFaceMat, Material inBeamMat)
    {
        for (int i = 0; i < facesMR.Length; i++)
        {
            facesMR[i].material = normalMat;
            facesMR[i].transform.GetChild(0).gameObject.SetActive(false);

            if (facesMR[i].name == hitFace.name)
            {
                facesMR[i].material = inFaceMat;
                beamsMR[i].material = inBeamMat;
                hitFace.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    private void NullifyBullet()
    {
        bullet = null;
    }


    public bool GetBeamActive()
    {
        return beamActive;
    }



}
