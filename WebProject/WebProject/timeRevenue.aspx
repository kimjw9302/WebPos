﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="timeRevenue.aspx.cs" Inherits="WebProject.timeRevenue" %>

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

    <!-- Bootstrap Core CSS -->
    <link href="./CSS/bootstrap.min.css" rel="stylesheet" />

    <!-- MetisMenu CSS -->
    <link href="./CSS/metisMenu.min.css" rel="stylesheet" />

    <!-- Custom CSS -->
    <link href="./CSS/sb-admin-2.css" rel="stylesheet" />

    <!-- Morris Charts CSS -->
    <link href="./CSS/morris.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <!-- Bootstrap Core JavaScript -->
    <script src="./CSS/bootstrap.min.js"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="./CSS/metisMenu.min.js"></script>

    <!-- Morris Charts JavaScript -->
    <script src="./CSS/raphael.min.js"></script>
    <script src="./CSS/morris.min.js"></script>
    <script src="./CSS/morris-data.js"></script>

    <!-- Custom Theme JavaScript -->
    <script src="./CSS/sb-admin-2.js"></script>
    <script>


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
                            날짜를 선택하세요
                            <br />
                            <asp:TextBox runat="server" ID="DateSample" /><juice:Datepicker runat="server" TargetControlID="DateSample" DateFormat="yy/mm/dd" />
                            <asp:Label ID="Label1" runat="server" Text="~"></asp:Label>
                            <asp:TextBox ID="DateSample2" runat="server" ></asp:TextBox>
                            <juice:Datepicker runat="server" TargetControlID="DateSample2" DateFormat="yy/mm/dd"/>
                            <asp:Button ID="btnView" runat="server" Text="조회" Height="27px" Width="86px" OnClick="btnView_Click" />
                        </div>

                    </div>

                    <!-- /.panel-body -->
                </div>

                <div class="panel panel-default">
                    <div class="panel-heading">
                        시간대별 매출현황 조회
                    </div>
                  
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div id="dataTables-example_wrapper" class="dataTables_wrapper form-inline dt-bootstrap no-footer" width="60%">

                            <div class="row">
                                <div class="col-sm-8">
                                    <asp:GridView ID="timeGridview" runat="server" CellPadding="4" Width="100%" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
                                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" BorderStyle="None" />
                                        <RowStyle BackColor="White" ForeColor="#003399" />
                                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                        <SortedDescendingHeaderStyle BackColor="#002876" />
                                    </asp:GridView>
                                </div>
                            </div>

                        </div>
                        <!-- /.table-responsive -->

                    </div>
                    <!-- /.panel-body -->
                </div>

                <!-- /.panel -->
                <div class="panel panel-default">
                    <!-- /.panel-body -->
                </div>
                <!-- /.panel .chat-panel -->


            </div>
    </form>
</body>
</html>
