# Skmr.Editor

## Engine

The engine is the part that manages file input and output. It also manages the en-/decoding of the files with LibAV.

This will also be extendet for simple tasks to adjust the images like intrapolation for scaling images more effective.


## Analyzer

The analyzer should be used to gain further information from the imagedata itself. Tasks are to alter images to detect certain parts of images. For this tasks EMGU CV is used.

The second part of the analyzer is machine learning. We inted to use Keras to create modles that are trained. This should help to categorize clips more effective into categories like games. Also it would allow us to detect certain objects inside a image.

The goal is to automate decisionmaking for cutting videos.

## Director

The director Project is the link between engine and analyzer. This should simplify the process of getting image data to directions, like where a cut should be done inside a video.

  
