# Skmr.Editor

## Engine

The engine is the part that manages file input and output. It contains datastructures to read files and implements decoders to decode bitstreams of h264.

This will also be extendet for simple tasks to adjust the images like intrapolation for scaling images more effective.


## Analyzer

The analyzer should be used to gain further information from the imagedata itself. Tasks are to alter images to detect certain parts of images. For this tasks EMGU CV is used.

The second part of the analyzer is machine learning. We inted to use Keras to create modles that are trained. This should help to categorize clips more effective into categories like games. Also it would allow us to detect certain objects inside a image.

The goal is to automate decisionmaking for cutting videos.

## Director

The director Project is the link between engine and analyzer. This should simplify the process of getting image data to directions, like where a cut should be done inside a video.

## Images

Images are Functions that can be used to alter images aswell as generate new images.

## Samples
```csharp
public void EncodingWithRav1e()
{
    var outp = File.Open("out_file.ivf", FileMode.Create);

    IVideoEncoder rav1e = new Rav1e(width, height);
    Image<RGB>? frame = null;

    //Read Frames
    foreach(Image<RGB>? frame in getFrames())
    {
        //Encode Frames
        rav1e.SendFrame(frame);

        //Get Finished Frames
        var status = rav1e.ReceiveFrame(out byte[]? data);

        //If a frame is ready, write it to the file
        if (status == EncoderState.Success && data != null)
        {
            outp.Write(data, 0, data.Length);
        }
    }

    //No more new Frames
    rav1e.Flush();

    //Get Remaining Frames
    while (true)
    {
        var status = rav1e.ReceiveFrame(out byte[]? data);

        if (status == EncoderState.Ended) break;
        if (status == EncoderState.Success && data != null)
        {
            outp.Write(data, 0, data.Length);
        }
    }
}
```
  
