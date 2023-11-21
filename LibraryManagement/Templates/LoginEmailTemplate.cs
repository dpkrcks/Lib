using System.Text;

namespace LibraryManagement.Templates
{
    public class LoginEmailTemplate
    {

        public string LoginHtmlTemplate(string username) { 
        
            var sb = new StringBuilder();

            sb.Append($@"
                 <!DOCTYPE html>
<html lang=""en"">
<head>
  <meta charset=""UTF-8"" />
  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
</head>
<body>
  <div style=""border:1px solid black ; width:600px; height:300px; "">
   <div style=""text-align:center ; margin-top:10px ;"">
   <h1 style=""color:#8A2BE2; font-weight:200"">
    Welcome to Library Management
   </h1>
   </div>
    <div style=""margin:0 10px; padding:0 5px"">
     <p style=""font-size:20px"">
       Hello {username},<br>
Library Management is the platform where you can find books to rent and also can help needy persons to donate books to empower them towards their goals in life.
Come and help society without much efforts.
 <br>
Warm regards,<br>
Team Library Management.
     </p>
     </div>
  </div>
  

</body>
</html>");

            return sb.ToString();
        }

             
    }
}
