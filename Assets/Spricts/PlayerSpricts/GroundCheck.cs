using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGround = false;

    private bool isGroundEnter = false;
    private bool isGroundStay = false;
    private bool isGroundExit = false;

     // ê⁄ínîªíË
    public bool IsGround()
    {
        if (isGroundEnter || isGroundStay)
        {
            isGround = true;
        }

        else if (isGroundExit)
        {
            isGround = false;
        }

        isGroundEnter = false;
        isGroundStay = false;   
        isGroundExit = false;

        return isGround;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGroundEnter = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGroundStay = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGroundExit = true;
        }
    }
    void Start()
    {

    }

    void Update()
    {
    }
}
