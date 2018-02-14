<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BoardList.aspx.cs" Inherits="PosBord.Bord" %>

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
            $("#GridView1").css('background-color', 'beige');
            $('th').css('text-align', 'center');
            });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
            <div class="col-sm-12">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="5" OnPageIndexChanging="GridView1_PageIndexChanging">
                <Columns>
                <asp:BoundField HeaderText="글번호" DataField="Num" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="제  목" ItemStyle-Width="600px" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# FuncStep(Eval("Step")) %>
                             <%--  asp코드를 사용하겠다  --%>
                            <a href='BoardView.aspx?Num=<%#Eval("Num")%>'>
                            <%#Eval("Title") %>
                            </a>
                            <%# GetNew(Eval("WriteDate")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:HyperLinkField HeaderText="제목" itemstyle-width="600px"  DataTextField="Title" DataNavigateUrlFormatString="~\BoardView.aspx?Eval('Num')"/>--%>
                    <asp:BoundField HeaderText="글쓴이" DataField="NickName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="작성일" DataField="WriteDate" DataFormatString="{0:yyyy-MM-dd}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="조회" DataField="Count" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="추천" DataField="Recommand" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                 </Columns>
            </asp:GridView>
            <asp:Button ID="btnWirte" runat="server" Text="글쓰기" OnClick="btnWirte_Click" />
            검색 : <asp:DropDownList ID="lstSearchField" runat="server">
                    <asp:ListItem>닉네임</asp:ListItem>
                    <asp:ListItem>제목</asp:ListItem>
                 <asp:ListItem>내용</asp:ListItem>
                 </asp:DropDownList>                
            <asp:TextBox ID="txtSearchContent" runat="server" ></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="검색" OnClick="btnSearch_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
