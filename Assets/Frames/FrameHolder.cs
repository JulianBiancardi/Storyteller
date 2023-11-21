using UnityEngine;

public class FrameHolder : BasicHolder
{
    public AudioClip onPlaceFrame;
    private AudioSource audioSource;
    private GameObject currentFrame;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void Receive(Selection selection)
    {
        if(selection.IsActor()){
            Debug.Log("Actor received in frame holder");
            return;
        }

        SpawnSet(selection.frameSet);
    }

    public void SpawnSet(FrameSet frameSet){
        Destroy(currentFrame);
        GameObject frame = FrameFactory.CreateFrame(frameSet);
        frame.transform.parent = transform;
        frame.transform.localScale = new Vector3(1, 1, 1);
        frame.transform.localPosition = new Vector3(0, 0, 0);
        currentFrame = frame;
        audioSource.clip = onPlaceFrame;
        audioSource.Play();
    }

    public FrameResult GetFrameResult(){
        if(currentFrame == null){
            return new FrameResult();
        }
        return currentFrame.GetComponent<Frame>().Compute();
    }
}
