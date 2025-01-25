using UnityEngine;

public class FieldBox : MonoBehaviour
{
    [Header("Wind Settings")]
    [SerializeField]
    private float windForce = 5f;

    public float WindForce
    {
        get { return windForce; }
        set { windForce = value; }
    }
}
