<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BoardWrite.aspx.cs" Inherits="PosBord.BoardWrite" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css"/>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
  <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
             <div class="row">
                <div class="col-sm-12" >
                    <asp:TextBox ID="tboxTitle" runat="server" Width="300px"></asp:TextBox><br />
                    <asp:TextBox ID="tboxContent" runat="server" Width="300px" Height="300px"></asp:TextBox><br />
                    <asp:Button ID="btnOk" runat="server" Text="확인" Width="47px" OnClick="btnOk_Click"/><asp:Button ID="btnCancle" runat="server" Text="취소" OnClick="btnCancle_Click"/>
                    <asp:FileUpload ID="ImgFile" runat="server" /><br />
                </div>                
            </div>
        </div>
    </form>
</body>
</html>
