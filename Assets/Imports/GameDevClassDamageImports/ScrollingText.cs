using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingText : MonoBehaviour
{
    [SerializeField] float duration = 1f;
    [SerializeField] float speed = 1f;

    private TextMesh textMesh;
    private float startTime;

    void Awake()
    {
        textMesh = GetComponent<TextMesh>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime < duration)
        {
            //Scroll up
            transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);

            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else
        {
            //Destroy
            Destroy(gameObject);
        }
    }

    public void SetText(string text)
    {
        textMesh.text = text;
    }

    public void SetColor(Color color)
    {
        textMesh.color = color;
    }
}
