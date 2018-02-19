<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="genderChart.aspx.cs" Inherits="WebProject.pages.genderChart" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />


    <title>SB Admin 2 - Bootstrap Admin Theme</title>


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
    <link href="../vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />

    <!-- MetisMenu CSS -->
    <link href="../vendor/metisMenu/metisMenu.min.css" rel="stylesheet" />

    <!-- Custom CSS -->
    <link href="../dist/css/sb-admin-2.css" rel="stylesheet" />

    <!-- Morris Charts CSS -->
    <link href="../vendor/morrisjs/morris.css" rel="stylesheet" />

    <!-- Custom Fonts -->
    <link href="../vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script>
        alert
    </script>
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
                    <a class="navbar-brand" href="index.html">SB Admin v2.0</a>
                </div>
                <!-- /.navbar-header -->



                <div class="navbar-default sidebar" role="navigation">
                    <div class="sidebar-nav navbar-collapse">
                        <ul class="nav" id="side-menu">
                            <li>

                                <asp:Label ID="lblUserID" runat="server" Font-Size="Medium"></asp:Label>
                                <!-- /input-group -->
                            </li>
                            <li>
                                <a href="index.html"><i class="fa fa-dashboard fa-fw"></i>게시판으로</a>
                            </li>
                            <li>
                                <a href="#"><i class="fa fa-bar-chart-o fa-fw"></i>매출 현황<span class="fa arrow"></span></a>
                                <ul class="nav nav-second-level">
                                    <li>
                                        <a href="flot.html">시간대 별 매출현황</a>
                                    </li>
                                    <li>
                                        <a href="morris.html">카테고리 별 매출현황</a>
                                    </li>
                                    <li>
                                        <a href="morris.html">품묵 별 매출현황</a>
                                    </li>
                                    <li>
                                        <a href="morris.html">결제수단 별 매출현황</a>
                                    </li>
                                    <li>
                                        <a href="morris.html">객층분석</a>
                                    </li>
                                    <li>
                                        <a href="morris.html">총매출 보기</a>
                                    </li>
                                </ul>
                                <!-- /.nav-second-level -->
                            </li>
                            <li>
                                <a href="tables.html"><i class="fa fa-table fa-fw"></i>재고 보기</a>
                            </li>
                        </ul>
                    </div>
                    <!-- /.sidebar-collapse -->
                </div>
                <!-- /.navbar-static-side -->
            </nav>
        </div>
        <div id="page-wrapper">

            <div class="panel panel-default">
                <div>
                    &nbsp; &nbsp;
  
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body">
                    <!-- /.list-group -->
                    <div>
                        &nbsp; &nbsp;
                            날짜를 선택하세요 ▶  &nbsp;
                        <asp:TextBox runat="server" ID="DateSample" Font-Size="Medium"/><juice:Datepicker ID="DateStart" runat="server" TargetControlID="DateSample" DateFormat="yy/mm/dd" />
                        <asp:Label ID="Label1" runat="server" Text="~" Font-Size="Large"></asp:Label>
                        <asp:TextBox ID="DateSample2" runat="server" Font-Size="Medium"></asp:TextBox>
                        <juice:Datepicker runat="server" TargetControlID="DateSample2" DateFormat="yy/mm/dd" />
                        <asp:Button ID="btnView" runat="server" Text="조회" Height="27px" Width="86px" OnClick="btnView_Click" />
                    </div>

                </div>
            </div>
            <!-- /.panel-body -->


            <div class="panel panel-default">
                <div class="panel-heading" id="ChartDiv">
                    <a style="font-size:x-large; text-decoration:none " >성별 차트 & 연령별 차트</a>
                    
                </div>

                <div class="row col-lg-6" id="ctGender" style="height:500px ; margin-top:150px">
                    <asp:Label CssClass="col-lg-6 text-center" runat="server" ID="msg" Font-Size="XX-Large"></asp:Label>
                </div>

                <div class="row col-lg-6" id="ctAge" style="height:500px ; margin-top:150px">
                </div>
            </div>

        </div>
    </form>
</body>
</html>
