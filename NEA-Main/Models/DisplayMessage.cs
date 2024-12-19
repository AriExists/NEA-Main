using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NEA_Main.Models.Generated;

namespace NEA_Main.Models
{
    public class DisplayMessage : Generated.Message
    {
		private bool _hasImage;

		public bool HasImage
		{
			get { return _hasImage; }
			set { _hasImage = value; }
		}

		private string? _imageUrl;

		public string? ImageUrl
		{
			get { return _imageUrl; }
			set { _imageUrl = value; }
		}

		private string _senderBio;			

		public string SenderBio
		{
			get { return _senderBio; }
			set { _senderBio = value; }
		}

		public AccountUser SenderAccount { get; set; }

	}
}
