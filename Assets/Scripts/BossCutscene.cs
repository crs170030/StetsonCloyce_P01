using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCutscene : MonoBehaviour
{
    [SerializeField] BossController _bossCon = null;
    [SerializeField] HealthBase _hb = null;
    [SerializeField] Transform cam = null;
    [SerializeField] AudioClip _scream = null;

    public float slowTime = 0.1f;
    Vector3 normalCamPos;

    // Start is called before the first frame update
    void Awake()
    {
        if (_hb != null)
        {
            //subscribe to damage event
            _hb.HalfHealth += StartCutscene;
        }
        else
        {
            Debug.Log("AAAH! HealthBase is Null!");
        }
        normalCamPos = cam.position;
    }

    void StartCutscene(int i)
    {
        Debug.Log("Starting cutscene");
        StartCoroutine("Cutscene");
    }

    IEnumerator Cutscene()
    {
        _hb.flashActive = false;
        //slow time
        Time.timeScale = slowTime;
        //move camera to boss
        cam.position = transform.position + new Vector3(0, 10, -5);
        //yield return new WaitForSeconds(.1f);

        //call boss script to turn around
        _bossCon.TurnAround();
        yield return new WaitForSeconds(.15f);

        //call boss script to change form
        _bossCon.ChangeForm();
        if(_scream != null)
            AudioHelper.PlayClip2D(_scream, 1f);
        yield return new WaitForSeconds(.25f);

        //call script to change attack speed values
        _bossCon.AngryTime();
        //move camera back to original position
        cam.position = normalCamPos;
        //resume time
        Time.timeScale = 1f;
        _hb.flashActive = true;
    }
}
