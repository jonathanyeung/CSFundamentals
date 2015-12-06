using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interviews.OODesign
{
    // Represents a collection of photos that can be used to create a mosaic.
    public class PhotoSet
    {
        public int pictureCount;
        public List<string> pictureTags;

        public Dictionary<int, Picture> pictures;

        public PhotoSet()
        {
            pictures = new Dictionary<int, Picture>();
        }

        public bool UploadPicture(File image)
        {
            var newPicture = new Picture(image);

            pictures.Add(newPicture.AverageImageColor, newPicture);

            return true;
        }       
    }

    // Represents a picture, as well as calculated metadata
    public class Picture
    {
        public File imageRes;
        public int length;
        public int width;
        public double fileSize;
        public int AverageImageColor;

        public Picture(File image)
        {
            throw new NotImplementedException();
        }
    }
    // From Problem 19.1, Elements Book pg. 150
    public class PhotoMosaic
    {
        
        private Dictionary<string, PhotoSet> _photoSets;

        public PhotoMosaic()
        {
            _photoSets = new Dictionary<string, PhotoSet>();
        }
        public File CreatePhotoMosaic(File targetPicture)
        {
            return null;
        }

        public File CreatePhotoMosaic(File targetPicture, PhotoSet set, int resolution)
        {
            Debug.Assert(set.pictures.Count > resolution);

            // Generate a 2D array of the image broken down to its specified resolution.  
            // The array contains the average color value of that block.
            int[,] closestColor = new int[resolution, resolution];


        }
    }

    public static class PhotoCalculator
    {
        public int ComputeAverageColor(File image)
        {
            throw new NotImplementedException();
        }

        public int ComputeAverageColorForSpecificPoint(File image, int x, int y, int totalX, int totalY)
        {
            return -1;
        }
    }

    public class Database
    {
        public File GetImage(Guid id)
        {
            return null;
        }

        public Guid StoreImage(File image)
        {
            throw new NotImplementedException();
        }
    }

}
