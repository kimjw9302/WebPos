<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllTotalRevenue.aspx.cs" Inherits="WebProject.AllTotalRevenue" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />


    <title>::Goodee24 - 관리자 홈페이지::</title>

    <!-- jQuery -->
    <script src="../vendor/jquery/jquery.min.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="../vendor/bootstrap/js/bootstrap.min.js"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="../vendor/metisMenu/metisMenu.min.js"></script>

    <!-- Morris Charts JavaScript -->
    <script src="../vendor/raphael/raphael.min.js"></script>
    <script src="../vendor/morrisjs/morris.min.js"></script>
<%--    <script src="../data/morris-data.js"></script>--%>

    <!-- Custom Theme JavaScript -->
    <script src="../dist/js/sb-admin-2.js"></script>
    <!-- Bootstrap Core CSS -->
    <link href="../vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />

    <!-- MetisMenu CSS -->
    <link href="../vendor/metisMenu/metisMenu.min.css" rel="stylesheet" />

    <!-- Custom CSS -->
    <link href="../dist/css/sb-admin-2.css" rel="stylesheet" />

    <!-- Morris Charts CSS -->
    <link href="../vendor/morrisjs/morris.css" rel="stylesheet" />

    <!-- Custom Fonts -->
    <link href="../vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />

          <!-- DataTables CSS -->
    <link href="../vendor/datatables-plugins/dataTables.bootstrap.css" rel="stylesheet"/>

    <!-- DataTables Responsive CSS -->
    <link href="../vendor/datatables-responsive/dataTables.responsive.css" rel="stylesheet"/>

    <script>


</script>
    <style>
        #lblUserID {
            float: left;
            margin-top: 8px;
        }

        #btn_Logout {
        }
    </style>
</head>
<body>


    <form id="form1" runat="server">
        <asp:ScriptManager ID="_Script" runat="server"></asp:ScriptManager>

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
                    <a class="navbar-brand" href="Main.aspx" style="color: #FF5E00">Goodee 24</a>
                </div>
                <ul class="nav navbar-top-links navbar-right" style="margin-top: 15px;">
                    <li>

                        <asp:Label ID="modeTxt" runat="server" Text="모드 상태"></asp:Label>

                    </li>
                </ul>
                <!-- /.navbar-header -->
                <div class="navbar-default sidebar" role="navigation">
                    <div class="sidebar-nav navbar-collapse">
                        <ul class="nav" id="side-menu">
                            <li style="padding: 10px 5px">
                                <asp:Label ID="lblUserID" runat="server" Font-Size="Medium" Width="70%"></asp:Label>

                                <asp:Button ID="btn_Logout" class="btn btn-default" runat="server" Text="Logout" OnClick="btn_Logout_Click" />


                            </li>
                        <li>
                            <a href="Main.aspx"><i class="fa fa-dashboard fa-fw"></i> 대쉬보드</a>
                        </li>
                        <li>
                            <a href="BoardList.aspx"><i class="fa fa-dashboard fa-fw"></i> 게시판으로</a>
                        </li>
                            <li>
                                <a href="#"><i class="fa fa-bar-chart-o fa-fw"></i>매출 현황<span class="fa arrow"></span></a>
                                <ul class="nav nav-second-level">
                                    <li>
                                        <a href="timeRevenue.aspx">시간대 별 매출현황</a>
                                    </li>
                                    <li>
                                        <a href="#">카테고리 별 매출현황</a>
                                    </li>
                                    <li>
                                        <a href="ProductsRevenue.aspx">품묵 별 매출현황</a>
                                    </li>
                                    <li>
                                        <a href="#">결제수단 별 매출현황</a>
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
                                <a href="#"><i class="fa fa-table fa-fw"></i>재고 보기</a>
                            </li>
                        </ul>
                    </div>
                    <!-- /.sidebar-collapse -->
                </div>
                <!-- /.navbar-static-side -->
            </nav>

            <div id="page-wrapper">
                <div class="row">
                    <div class="row">
                        <div class="col-lg-12">
                            <h1 class="page-header">총 매출 현황</h1>
                        </div>
                        <!-- /.col-lg-12 -->
                    </div>
                    <div class="row">
                        <!-- /.col-lg-8 -->

                        <div class="panel panel-default">
                            <div>
                                &nbsp; &nbsp;
  
                            </div>
                            <!-- /.panel-heading -->
                            <div class="panel-body">
                                <!-- /.list-group -->
                                <div>
                                    &nbsp; &nbsp;
                            <asp:TextBox runat="server" ID="DateSample" /><juice:Datepicker runat="server" TargetControlID="DateSample" />
                                    <asp:Label ID="Label1" runat="server" Text="~"></asp:Label>
                                    <asp:TextBox ID="DateSample2" runat="server"></asp:TextBox>
                                    <juice:Datepicker runat="server" TargetControlID="DateSample2" />
                                    <asp:Button ID="Button1" runat="server" Text="조회" Height="27px" Width="86px" OnClick="Button1_Click" />
                                </div>

                            </div>
                            <!-- /.panel-body -->
                        </div>
                        <!-- /.panel -->
                        <div class="panel panel-default">
                            <!-- /.panel-body -->
                        </div>
                        <!-- /.panel .chat-panel -->

                    </div>
                    <!-- /.row -->
                    <!-- /.row -->
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    총 매출 현황 테이블
                                </div>
                                <!-- /.panel-heading -->
                                <div class="panel-body" id="tablediv">

                                    <!-- /.table-responsive -->

                                </div>
                                <!-- /.panel-body -->
                            </div>
                            <!-- /.panel -->
                        </div>
                        <!-- /.col-lg-12 -->
                    </div>
                    <!-- /.row -->
                    <div class="panel-heading">
                        총 매출 현황 차트
                    </div>
                    <div class="row" id="morrisChart">
                    </div>
                    <!-- /.row -->

                </div>
                <!-- /#page-wrapper -->

            </div>
    </form>
</body>
</html>
