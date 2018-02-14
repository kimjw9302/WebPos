<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BoardView.aspx.cs" Inherits="PosBord.BoardView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
    
    <script>
        $(document).ready(function () {
        //    var tbox = tag.children[0].children[1].children[0].children[2].id;
        //    var but = tag.children[0].children[1].children[0].children[3].id;
            //$("#" + but).css('visibility', 'hidden');
            $("#GridView1").css('border', 'white');            
            $('td').css('border-bottom-style', 'solid');
            //$("#GridView1").$('input').css('display', 'none');
            
            $('a').click(function () {                
                //$('input').css('display', 'initial');
                $(this.parentElement.previousElementSibling.children[2]).toggle();
                $(this.parentElement.previousElementSibling.children[3]).toggle();
                //var Num = this.parentElement.previousElementSibling.children[2].id.substring(-1, 1);
                alert(this.parentElement.previousElementSibling.children[3].id)
                var ReplyNum = '{ReplyNum:' + this.parentElement.previousElementSibling.children[2].id.substr(-1, 1) + '}';
                //alert(Num)
                
                $.ajax({
                    url: 'BoardView.aspx/ReplyR',
                    type: 'post',
                    data: ReplyNum,
                    contentType: 'application/JSON',
                    success: function (response) {
                        console.log(response.d);
                    }
                });
                
            });
            
        });
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                제목   : <asp:Label ID="lblTitle" Text="" runat="server" Width="500px"/><br />      
                닉네임 : <asp:Label ID="lblNickName" Text="" runat="server" />
                조회수 : <asp:Label ID="lblCount" runat="server" Text=""></asp:Label>
                추천 : <asp:Label ID="lblLike" runat="server" Text=""></asp:Label>
                작성일 : <asp:Label ID="lblDate" runat="server" Text=""></asp:Label><br />
            </div>        
            <div>
                내용   : <asp:Label ID="lblContent" Text="" runat="server" /><br />
            </div>
            <div >
                <%--댓글 부분 --%>
                <asp:GridView ID="GridView1" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AutoGenerateColumns="false">
                    <Columns>                   
                    <asp:TemplateField ItemStyle-Width="600px" >
                        <ItemTemplate>                            
                            <%#Eval("NickName") %> <%#Eval("ReplyDate") %><br />
                            <%#Eval("Content")%><br />
                            <asp:TextBox ID="tboxReplyR" runat="server" Visible="true"></asp:TextBox>
                            <asp:Button ID="btnReplyRWrite" runat="server" Text="작성" OnClick="btnReplyRWrite_Click" Visible="true" />                           
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField>
                        <ItemTemplate>                            
                            <a name="a" id="btnReplyRWrite" style="cursor:pointer" >답변</a>                            
                        </ItemTemplate>
                    </asp:TemplateField>                        
                        <asp:TemplateField>
                        <ItemTemplate>                            
                            <a>신고</a>
                        </ItemTemplate>
                    </asp:TemplateField>                        
                    </Columns>                    
                </asp:GridView>           
            
                <asp:TextBox ID="tboxReply" runat="server" Width="600px" height="70px" Visible="true"></asp:TextBox>
                <asp:Button ID="btnWriter" runat="server" Text="등록" Width="67px" Height="70px" OnClick="btnWriter_Click" />
            </div>
            <div>
                <asp:Button ID="btnReply" runat="server" Text="답변" OnClick="btnReply_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="수정" OnClick="btnUpdate_Click"/>
                <asp:Button ID="btnDelete" runat="server" Text="글삭제" OnClick="btnDelete_Click"/>
                <asp:Button ID="btnList" runat="server" Text="리스트" OnClick="btnList_Click"/>
                <asp:Button ID="btnLike" runat="server" OnClick="btnLike_Click" Text="추천" />
            </div>
        </div>
    </form>
</body>
</html>
