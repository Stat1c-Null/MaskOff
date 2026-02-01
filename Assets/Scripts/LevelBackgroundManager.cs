using UnityEngine;

public class LevelBackgroundManager : MonoBehaviour
{
    [SerializeField] private Transform columnToDuplicate; // Assign your column in Inspector
    [SerializeField] private int numberOfDuplicates = 3;
    
    private float columnWidth;

    void Start()
    {
        if (columnToDuplicate == null)
        {
            Debug.LogError("Please assign a column to duplicate!");
            return;
        }

        // Calculate column width from the column's child sprite
        SpriteRenderer sr = columnToDuplicate.GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            columnWidth = sr.bounds.size.x;
        }

        // Duplicate the column to the right
        for (int i = 1; i <= numberOfDuplicates; i++)
        {
            Vector3 newPosition = columnToDuplicate.position + new Vector3(columnWidth * i, 0, 0);
            GameObject duplicate = Instantiate(columnToDuplicate.gameObject, newPosition, Quaternion.identity, transform);
            duplicate.name = columnToDuplicate.name + "_" + i;
        }
    }
}

