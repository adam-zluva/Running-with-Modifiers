using UnityEngine;

[CreateAssetMenu(menuName = "Sound")]
public class Sound : ScriptableObject
{
    [SerializeField] private AudioClip[] clipVariations;
    public AudioClip clip => clipVariations.GetRandom();

    [SerializeField, Range(0f, 1f)] private float _volume = 1f;
    public float volume { get => _volume; }

    [SerializeField] private Vector2 _pitchRange = new Vector2(1f, 1f);
    public Vector2 pitchRange { get { return _pitchRange; } }
    public float pitch => Random.Range(pitchRange.x, pitchRange.y);

    public AudioClip GetClip(int index)
    {
        index = Mathf.Clamp(index, 0, clipVariations.Length);
        return clipVariations[index];
    }
}
