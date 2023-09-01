using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    public TextMeshProUGUI scoreAmountLabel;
    Vector3 mousePos;
    Rigidbody ball;
    public float speed = 0.1f;
    private bool ballAlreadyTouchedGround=false;
    private float slideStartedPosY=0.8f;
    private float slideEndedPosY;
    private float slideForce;
    private int currentScore = 0;
    private bool goalCanBeScored = true;

    private List<float> ballPosY = new List<float>();
    
    // Start is called before the first frame update
    void Start()
    {
        ball = GetComponent<Rigidbody>();
        ball.useGravity = false;
        for (int i = 0; i < 100; i++)
        {
            ballPosY.Add(0.8f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ballPosY.Add(ball.transform.position.y);
            
            mousePos = Input.mousePosition;
            mousePos.z = speed;
            transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        }
        if(Input.GetMouseButtonUp(0))
        {
            slideStartedPosY = ballPosY[ballPosY.Count - 40];
            slideEndedPosY = ballPosY[ballPosY.Count-1];
            slideForce = (slideEndedPosY - slideStartedPosY)*5;
            ball.useGravity = true;
            ball.AddForce(new Vector3(0f,slideForce*1f,slideForce*1f),ForceMode.Impulse);
        }

        if (ball.transform.position.y < 0.35f)
        {
            if (!ballAlreadyTouchedGround)
            {
                ballPosY.Clear();
                for (int i = 0; i < 100; i++)
                {
                    ballPosY.Add(0.8f);
                }
                
                ballAlreadyTouchedGround=true;

                goalCanBeScored = true;
                    
                StartCoroutine(ballThrownTimer());
            }
        }

        if (ball.transform.position.x > -0.1f && ball.transform.position.x < 0.1f&&
            ball.transform.position.y > 1.31f && ball.transform.position.y < 1.34f&&
            ball.transform.position.z > -7f && ball.transform.position.z < -6.8f)
        {
            if (goalCanBeScored)
            {
                goalCanBeScored = false;
                currentScore++;
                scoreAmountLabel.text = currentScore.ToString();
            }
        }
        
    }

    IEnumerator ballThrownTimer()
    {
        yield return new WaitForSeconds(2);

        ball.useGravity = false;
        ball.velocity = Vector3.zero;
        ball.angularVelocity = Vector3.zero;
        transform.position = new Vector3(0f, 0.8f, -7.38f);
        ballAlreadyTouchedGround = false;
    }
}
