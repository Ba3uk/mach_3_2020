using Data;
using UnityEngine;

public class ArtCollection : MonoBehaviour
{
    public static ArtCollection Instance { private set; get; }

    [SerializeField] private ArtConfig artConfig;
    public ArtConfig ArtConfig => artConfig;

    private void Awake()
    {
        Instance = this;
    }
}
