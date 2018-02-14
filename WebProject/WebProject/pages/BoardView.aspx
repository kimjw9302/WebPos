<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BoardView.aspx.cs" Inherits="PosBord.BoardView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>  
    
    <title>글보기</title>
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
                //alert(this.parentElement.previousElementSibling.children[3].id)
            });
        });
            //$('#GridView1').('a').css('display', 'none');
            //    click(function () {                
            //    var ReplyNum = '{ReplyNum:' + this.id.substr(-1, 1) + '}';

            //    //$.ajax({
            //    //    url: 'BoardView.aspx/ReplyR',
            //    //    type: 'post',
            //    //    data: ReplyNum,
            //    //    contentType: 'application/JSON',
            //    //    success: function (response) {
            //    //        console.log(response.d);
            //    //    }

            //    //});
            //    //alert(this.id.substr(-1, 1));
            //}) 
    </script>
  <!-- jQuery -->
    <script src="../vendor/jquery/jquery.min.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="../vendor/bootstrap/js/bootstrap.min.js"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="../vendor/metisMenu/metisMenu.min.js"></script>

    <!-- Morris Charts JavaScript -->
    <script src="../vendor/raphael/raphael.min.js"></script>
    <script src="../vendor/morrisjs/morris.min.js"></script>
    <script src="../data/morris-data.js"></script>

    <!-- Custom Theme JavaScript -->
    <script src="../dist/js/sb-admin-2.js"></script>
    <!-- Bootstrap Core CSS -->
    <link href="../vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet"/>

    <!-- MetisMenu CSS -->
    <link href="../vendor/metisMenu/metisMenu.min.css" rel="stylesheet"/>

    <!-- Custom CSS -->
    <link href="../dist/css/sb-admin-2.css" rel="stylesheet"/>

    <!-- Morris Charts CSS -->
    <link href="../vendor/morrisjs/morris.css" rel="stylesheet"/>

    <!-- Custom Fonts -->
    <link href="../vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>

    <style>
        #lblUserID {
            float:left;
            margin-top:8px;
        }
        #btn_Logout {
 

        }
    </style>
</head>
<body>
    <form runat ="server" method="get">     
    <div id="wrapper">
        <!-- Navigation -->
        <nav class="navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: 0">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="Main.aspx" style="color:#FF5E00" >Goodee 24</a>
            </div>
                <ul class="nav navbar-top-links navbar-right" style="margin-top:15px;">
                    <li>
               
                        <asp:Label ID="modeTxt" runat="server" Text="모드 상태"></asp:Label>
                    
                           </li>  
                    </ul>
            <%--<!-- /.navbar-header --- 여기  -->

        
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                        <i class="fa fa-envelope fa-fw"></i> <i class="fa fa-caret-down"></i>
                    </a>
                    <ul class="dropdown-menu dropdown-messages">
                        <li>
                            <a href="#">
                                <div>
                                    <strong>John Smith</strong>
                                    <span class="pull-right text-muted">
                                        <em>Yesterday</em>
                                    </span>
                                </div>
                                <div>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque eleifend...</div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="#">
                                <div>
                                    <strong>John Smith</strong>
                                    <span class="pull-right text-muted">
                                        <em>Yesterday</em>
                                    </span>
                                </div>
                                <div>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque eleifend...</div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="#">
                                <div>
                                    <strong>John Smith</strong>
                                    <span class="pull-right text-muted">
                                        <em>Yesterday</em>
                                    </span>
                                </div>
                                <div>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque eleifend...</div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a class="text-center" href="#">
                                <strong>Read All Messages</strong>
                                <i class="fa fa-angle-right"></i>
                            </a>
                        </li>
                    </ul>
                    <!-- /.dropdown-messages -->
                </li>
                <!-- /.dropdown -->
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                        <i class="fa fa-tasks fa-fw"></i> <i class="fa fa-caret-down"></i>
                    </a>
                    <ul class="dropdown-menu dropdown-tasks">
                        <li>
                            <a href="#">
                                <div>
                                    <p>
                                        <strong>Task 1</strong>
                                        <span class="pull-right text-muted">40% Complete</span>
                                    </p>
                                    <div class="progress progress-striped active">
                                        <div class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: 40%">
                                            <span class="sr-only">40% Complete (success)</span>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="#">
                                <div>
                                    <p>
                                        <strong>Task 2</strong>
                                        <span class="pull-right text-muted">20% Complete</span>
                                    </p>
                                    <div class="progress progress-striped active">
                                        <div class="progress-bar progress-bar-info" role="progressbar" aria-valuenow="20" aria-valuemin="0" aria-valuemax="100" style="width: 20%">
                                            <span class="sr-only">20% Complete</span>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="#">
                                <div>
                                    <p>
                                        <strong>Task 3</strong>
                                        <span class="pull-right text-muted">60% Complete</span>
                                    </p>
                                    <div class="progress progress-striped active">
                                        <div class="progress-bar progress-bar-warning" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 60%">
                                            <span class="sr-only">60% Complete (warning)</span>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="#">
                                <div>
                                    <p>
                                        <strong>Task 4</strong>
                                        <span class="pull-right text-muted">80% Complete</span>
                                    </p>
                                    <div class="progress progress-striped active">
                                        <div class="progress-bar progress-bar-danger" role="progressbar" aria-valuenow="80" aria-valuemin="0" aria-valuemax="100" style="width: 80%">
                                            <span class="sr-only">80% Complete (danger)</span>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a class="text-center" href="#">
                                <strong>See All Tasks</strong>
                                <i class="fa fa-angle-right"></i>
                            </a>
                        </li>
                    </ul>
                    <!-- /.dropdown-tasks -->
                </li>
                <!-- /.dropdown -->
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                        <i class="fa fa-bell fa-fw"></i> <i class="fa fa-caret-down"></i>
                    </a>
                    <ul class="dropdown-menu dropdown-alerts">
                        <li>
                            <a href="#">
                                <div>
                                    <i class="fa fa-comment fa-fw"></i> New Comment
                                    <span class="pull-right text-muted small">4 minutes ago</span>
                                </div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="#">
                                <div>
                                    <i class="fa fa-twitter fa-fw"></i> 3 New Followers
                                    <span class="pull-right text-muted small">12 minutes ago</span>
                                </div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="#">
                                <div>
                                    <i class="fa fa-envelope fa-fw"></i> Message Sent
                                    <span class="pull-right text-muted small">4 minutes ago</span>
                                </div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="#">
                                <div>
                                    <i class="fa fa-tasks fa-fw"></i> New Task
                                    <span class="pull-right text-muted small">4 minutes ago</span>
                                </div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="#">
                                <div>
                                    <i class="fa fa-upload fa-fw"></i> Server Rebooted
                                    <span class="pull-right text-muted small">4 minutes ago</span>
                                </div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a class="text-center" href="#">
                                <strong>See All Alerts</strong>
                                <i class="fa fa-angle-right"></i>
                            </a>
                        </li>
                    </ul>
                    <!-- /.dropdown-alerts -->
                </li>
                <!-- /.dropdown -->
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                        <i class="fa fa-user fa-fw"></i> <i class="fa fa-caret-down"></i>
                    </a>
                    <ul class="dropdown-menu dropdown-user">
                        <li><a href="#"><i class="fa fa-user fa-fw"></i> User Profile</a>
                        </li>
                        <li><a href="#"><i class="fa fa-gear fa-fw"></i> Settings</a>
                        </li>
                        <li class="divider"></li>
                        <li><a href="login.html"><i class="fa fa-sign-out fa-fw"></i> Logout</a>
                        </li>
                    </ul>
                    <!-- /.dropdown-user -->
                </li>
                <!-- /.dropdown -->
            </ul>--%>
            <!-- /.navbar-top-links -->

            <div class="navbar-default sidebar" role="navigation">
                <div class="sidebar-nav navbar-collapse">
                    <ul class="nav" id="side-menu">
                        <li  style="padding: 10px 5px">                       
                            <asp:Label ID="lblUserID" runat="server" Font-Size="Medium" Width="70%" ></asp:Label>
                                   
                                <asp:Button ID="btn_Logout"  class="btn btn-default" runat="server" Text="Logout" OnClick="btn_Logout_Click"/>
                          
                       
                            <!-- /input-group -->
                        </li>
                           <li>
                            <a href="Main.aspxl"><i class="fa fa-dashboard fa-fw"></i> 대쉬보드</a>
                        </li>
                        <li>
                            <a href="BoardList.aspx"><i class="fa fa-dashboard fa-fw"></i> 게시판으로</a>
                        </li>
                        <li>
                            <a href="#"><i class="fa fa-bar-chart-o fa-fw"></i> 매출 현황<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                                <li>
                                    <a href="#">시간대 별 매출현황</a>
                                </li>
                                <li>
                                    <a href="#l">카테고리 별 매출현황</a>
                                </li>
                                <li>
                                    <a href="ProductsRevenue.aspx">품목 별 매출현황</a>
                                </li>
                                <li>
                                    <a href="#l">결제수단 별 매출현황</a>
                                </li>
                                  <li>
                                    <a href="#">객층분석</a>
                                </li>
                                 <li>
                                    <a href="AllTotalRevenue.aspx">총매출 보기</a>
                                </li>
                            </ul>
                            <!-- /.nav-second-level -->
                        </li>
                        <li>
                            <a href="tables.html"><i class="fa fa-table fa-fw"></i> 재고 보기</a>
                        </li>                     
                    </ul>
                </div>
                <!-- /.sidebar-collapse -->
            </div>
            <!-- /.navbar-static-side -->
        </nav>

        <div id="page-wrapper">
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
                            <a>
                                <asp:TextBox ID="tboxReplyR" runat="server" Visible="true"></asp:TextBox>
                                <asp:Button ID="btnReplyRWrite" runat="server" Text="작성" OnClick="btnReplyRWrite_Click" Visible="true" />                           
                            </a>
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
        <!-- /#page-wrapper -->

    </div>
      </form>
    <!-- /#wrapper -->
</body>
</html>
