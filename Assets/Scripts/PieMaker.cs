using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

/// <summary>
/// Class to make Fractions
/// </summary>
public class PieMaker : MonoBehaviour
{
    /// <summary>
    /// Upper Bound for Generating fractions
    /// </summary>
    [SerializeField] int UpperBound;

    /// <summary>
    /// Space Between Each Fraction
    /// </summary>
    [SerializeField] float SpaceBetweenFraction;
    /// <summary>
    /// Tracking variable for total fractions
    /// </summary>
    int Denominator;
    /// <summary>
    /// Numerator of fraction
    /// </summary>
    int Numerator;

    /// <summary>
    /// Fraction value field
    /// </summary>
    [SerializeField] TextMeshProUGUI FractionValue;
    /// <summary>
    /// Correct added fractions
    /// </summary>
    int currentCount;
    /// <summary>
    /// List of all Fraction pies
    /// </summary>
    List<GameObject> Pies = new List<GameObject>();
    /// <summary>
    /// Reference for pie Gameobject
    /// </summary>
    public GameObject Pie;

    public static event Action OnCorrectAnswer;
    
    void Start()
    {   
        UpdateBoard();
    }

    /// <summary>
    /// Update game board
    /// </summary>
    public void UpdateBoard()
    {
        ClearGameCycle();
        Random rnd = new Random();
        Denominator = rnd.Next(2, UpperBound);
        Numerator = rnd.Next(2, Denominator);

        FractionValue.text = $"{Numerator} / {Denominator}";    
        CreateFractions(Denominator, SpaceBetweenFraction);
    }

    /// <summary>
    /// Clear previous Game Data
    /// </summary>
    void ClearGameCycle()
    {
        foreach(var i in Pies)
        {
            Destroy(i);
        }
        Pies.Clear();
        currentCount = 0;
    }
    /// <summary>
    /// On Click Add to Fraction
    /// </summary>
    public void AddToFraction() {
        if(currentCount < Denominator){
            currentCount++;
            UpdateFraction();
        }
    }

    /// <summary>
    /// Onclick subtract from fractions
    /// </summary>
    public void RemToFraction () {
        if(currentCount > 0){
            currentCount--;
            UpdateFraction();
        }
    }

    public void RaiseCorrectAnswer()
    {
        OnCorrectAnswer?.Invoke();
    }

    /// <summary>
    /// Update Fraction Color 
    /// </summary>
    void UpdateFraction()
    {
        for(int i = 0;i < Pies.Count; i++)
        {
            Pies[i].GetComponent<MeshRenderer>().material.color = DimColor(i);
        }
        if(currentCount == Numerator) {
            for(int i = 0; i < Numerator; i++){
                Pies[i].GetComponent<MeshRenderer>().material.color = Color.green;
            }
            RaiseCorrectAnswer();
        }
        else
        {
            for(int i = 0; i < currentCount; i++)
            {
                Pies[i].GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
    }
    /// <summary>
    /// Function to render fractions
    /// </summary>
    /// <param name="numberOfPie"></param>
    /// <param name="gapAngle"></param>
    void CreateFractions(int numberOfPie, float gapAngle)
    {
        float radius = 100f; // Radius of the pie
        float angleStep = 360f / numberOfPie;
        float angleoffSet = 90;
        //Pie.transform.position = new Vector3(26, 0, 70);
        for (int i = 0; i < numberOfPie; i++)
        {
            // Create GameObject for each slice
            GameObject slice = new GameObject($"Slice_{i}", typeof(MeshFilter), typeof(MeshRenderer));
            Pies.Add(slice);
            slice.transform.SetParent(Pie.transform, false);
            //slice.transform.localPosition = new Vector3(0, 0, 0);

            // Generate mesh
            Mesh mesh = new Mesh();
            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();

            // Center of the pie
            vertices.Add(Vector3.zero);

            // Add vertices for slice edges
            float startAngle = angleoffSet + i * angleStep + gapAngle / 2;
            float endAngle = angleoffSet + (i + 1) * angleStep - gapAngle / 2;

            for (float angle = startAngle; angle <= endAngle; angle += 1f)
            {
                float rad = Mathf.Deg2Rad * angle;
                vertices.Add(new Vector3(Mathf.Cos(rad) * radius, Mathf.Sin(rad) * radius, 0));
            }

            // Create triangles for the slice
            for (int j = 1; j < vertices.Count - 1; j++)
            {
                
                triangles.Add(j + 1);
                triangles.Add(j);
                triangles.Add(0);
            }

            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateNormals(); // Ensure lighting works

            // Assign mesh and material
            slice.GetComponent<MeshFilter>().mesh = mesh;
            slice.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Standard"))
            {
                color =  DimColor((float)i) //Color.grey // Unique color per slice
            };

            // Position the slice
            //slice.transform.position = Vector3.zero;
            //slice.transform.localScale = Vector3.one * 0.1f; // Adjust scale if necessary
        }
    }

    Color DimColor(float idx){
        return Color.HSVToRGB((float)idx / Denominator, 1, 1) * 0.5f ;
    }
}
