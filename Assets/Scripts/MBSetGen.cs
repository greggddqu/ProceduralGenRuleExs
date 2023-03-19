using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBSetGen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MBrot();
    }

    public void MBrot()
    {
        Renderer rend;
        float epsilon = 0.1f; // The step size across the X and Y axis defines the resolution of the set
        float x;
        float y;
        int maxIterations = 30; // increasing this will also increase detail of fractal

        //This is the Mandelbrot map using real numbers
        //could also create a 'complex' class to hold the complex type
        float Zr, Zrtemp; 
        float Zi;
        float Cr;
        float Ci;
        float modZ;
        int iterations;
        int i = 0;
        for (x = -2; x <= 2; x += epsilon)
        {
            i++; int j = 0;
            for (y = -2; y <= 2; y += epsilon)
            {
                j++;
                iterations = 0;
                Cr = x;
                Ci = y;
                Zr = 0;
                Zi = 0;
                modZ = Cr * Cr + Ci * Ci;
                while ( modZ <= 2*2 && iterations < maxIterations)
                {
                    Zrtemp = Zr * Zr - Zi * Zi + Cr;
                    Zi = 2.0f * Zi * Zr + Ci;
                    Zr = Zrtemp;
                    //Z = Z * Z + C; in complex notation
                    modZ = Zr * Zr + Zi * Zi;
                    iterations++;
                }
                if (modZ <= 2)
                {
                    //set the pixel to black or instantiate a game object or fill up a map
                    GameObject ptype = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    ptype.transform.Rotate(Vector3.right, 90.0f);
                    Vector3 thisPos = (float)i * Vector3.right + (float)j * Vector3.forward;
                    ptype.transform.position = thisPos;

                    rend = ptype.GetComponent<Renderer>();
                    Color color = Color.black;
                    rend.material.color = color;
                }
                else
                {
                    //depending on the number of iterations, color a pixel.
                    GameObject ptype = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    ptype.transform.Rotate(Vector3.right, 90.0f);
                    Vector3 thisPos = (float)i * Vector3.right + (float)j * Vector3.forward;
                    ptype.transform.position = thisPos;

                    Color colorStart = Color.red;
                    Color colorEnd = Color.green;
                    rend = ptype.GetComponent<Renderer>();
                    Color color = Color.Lerp(colorStart, colorEnd, (float)iterations / maxIterations);
                    rend.material.color = color;
                }

            }
        }
    }
}
