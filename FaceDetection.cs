using UnityEngine;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ObjdetectModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.UnityUtils;

public class FaceDetector : MonoBehaviour
{
    public BirdController birdController;
    private WebCamTexture webCamTexture;
    private CascadeClassifier cascade;
    private Mat rgbaMat;
    private Mat grayMat;
    private MatOfRect faces;

    void Start()
    {
        webCamTexture = new WebCamTexture();
        webCamTexture.Play();
        
        cascade = new CascadeClassifier();
        cascade.load(Utils.getFilePath("haarcascade_frontalface_default.xml"));
        
        rgbaMat = new Mat();
        grayMat = new Mat();
        faces = new MatOfRect();
    }

    void Update()
    {
        if (webCamTexture.isPlaying && webCamTexture.didUpdateThisFrame)
        {
            Utils.webCamTextureToMat(webCamTexture, rgbaMat, true, false);
            Imgproc.cvtColor(rgbaMat, grayMat, Imgproc.COLOR_RGBA2GRAY);
            
            cascade.detectMultiScale(grayMat, faces);
            
            if (faces.total() > 0)
            {
                OpenCVForUnity.CoreModule.Rect[] facesArray = faces.toArray();
                float faceY = (float)facesArray[0].y / grayMat.rows();
                birdController.Flap(1 - faceY); // Invert Y because OpenCV origin is top-left
            }
        }
    }
}
