
var camelCaseTokenizer = function (obj) {
    var previous = '';
    return obj.toString().trim().split(/[\s\-]+|(?=[A-Z])/).reduce(function(acc, cur) {
        var current = cur.toLowerCase();
        if(acc.length === 0) {
            previous = current;
            return acc.concat(current);
        }
        previous = previous.concat(current);
        return acc.concat([current, previous]);
    }, []);
}
lunr.tokenizer.registerFunction(camelCaseTokenizer, 'camelCaseTokenizer')
var searchModule = function() {
    var idMap = [];
    function y(e) { 
        idMap.push(e); 
    }
    var idx = lunr(function() {
        this.field('title', { boost: 10 });
        this.field('content');
        this.field('description', { boost: 5 });
        this.field('tags', { boost: 50 });
        this.ref('id');
        this.tokenizer(camelCaseTokenizer);

        this.pipeline.remove(lunr.stopWordFilter);
        this.pipeline.remove(lunr.stemmer);
    });
    function a(e) { 
        idx.add(e); 
    }

    a({
        id:0,
        title:"DeveloperDashboard",
        content:"DeveloperDashboard",
        description:'',
        tags:''
    });

    a({
        id:1,
        title:"bugtrackingapplicationDataSet",
        content:"bugtrackingapplicationDataSet",
        description:'',
        tags:''
    });

    a({
        id:2,
        title:"bugtrackingapplicationDataSet bugTableRowChangeEventHandler",
        content:"bugtrackingapplicationDataSet bugTableRowChangeEventHandler",
        description:'',
        tags:''
    });

    a({
        id:3,
        title:"AdminPanel",
        content:"AdminPanel",
        description:'',
        tags:''
    });

    a({
        id:4,
        title:"ManagerDashboard",
        content:"ManagerDashboard",
        description:'',
        tags:''
    });

    a({
        id:5,
        title:"bugtrackingapplicationDataSet adminTableRowChangeEvent",
        content:"bugtrackingapplicationDataSet adminTableRowChangeEvent",
        description:'',
        tags:''
    });

    a({
        id:6,
        title:"bugtrackingapplicationDataSet projectTableDataTable",
        content:"bugtrackingapplicationDataSet projectTableDataTable",
        description:'',
        tags:''
    });

    a({
        id:7,
        title:"UserChart",
        content:"UserChart",
        description:'',
        tags:''
    });

    a({
        id:8,
        title:"BugSolvedInformation",
        content:"BugSolvedInformation",
        description:'',
        tags:''
    });

    a({
        id:9,
        title:"HistoryInformation",
        content:"HistoryInformation",
        description:'',
        tags:''
    });

    a({
        id:10,
        title:"adminTableTableAdapter",
        content:"adminTableTableAdapter",
        description:'',
        tags:''
    });

    a({
        id:11,
        title:"userTableTableAdapter",
        content:"userTableTableAdapter",
        description:'',
        tags:''
    });

    a({
        id:12,
        title:"AdminChart",
        content:"AdminChart",
        description:'',
        tags:''
    });

    a({
        id:13,
        title:"TableAdapterManager",
        content:"TableAdapterManager",
        description:'',
        tags:''
    });

    a({
        id:14,
        title:"bugtrackingapplicationDataSet projectTableRowChangeEventHandler",
        content:"bugtrackingapplicationDataSet projectTableRowChangeEventHandler",
        description:'',
        tags:''
    });

    a({
        id:15,
        title:"bugtrackingapplicationDataSet userTableDataTable",
        content:"bugtrackingapplicationDataSet userTableDataTable",
        description:'',
        tags:''
    });

    a({
        id:16,
        title:"BugInformation",
        content:"BugInformation",
        description:'',
        tags:''
    });

    a({
        id:17,
        title:"BugStatus",
        content:"BugStatus",
        description:'',
        tags:''
    });

    a({
        id:18,
        title:"TesterDashboard",
        content:"TesterDashboard",
        description:'',
        tags:''
    });

    a({
        id:19,
        title:"bugtrackingapplicationDataSet bugTableRowChangeEvent",
        content:"bugtrackingapplicationDataSet bugTableRowChangeEvent",
        description:'',
        tags:''
    });

    a({
        id:20,
        title:"bugtrackingapplicationDataSet bugTableDataTable",
        content:"bugtrackingapplicationDataSet bugTableDataTable",
        description:'',
        tags:''
    });

    a({
        id:21,
        title:"bugtrackingapplicationDataSet userTableRow",
        content:"bugtrackingapplicationDataSet userTableRow",
        description:'',
        tags:''
    });

    a({
        id:22,
        title:"UserTable",
        content:"UserTable",
        description:'',
        tags:''
    });

    a({
        id:23,
        title:"bugtrackingapplicationDataSet adminTableRow",
        content:"bugtrackingapplicationDataSet adminTableRow",
        description:'',
        tags:''
    });

    a({
        id:24,
        title:"ReportTester",
        content:"ReportTester",
        description:'',
        tags:''
    });

    a({
        id:25,
        title:"TableAdapterManager UpdateOrderOption",
        content:"TableAdapterManager UpdateOrderOption",
        description:'',
        tags:''
    });

    a({
        id:26,
        title:"RegexUtilities",
        content:"RegexUtilities",
        description:'',
        tags:''
    });

    a({
        id:27,
        title:"ProjectInformation",
        content:"ProjectInformation",
        description:'',
        tags:''
    });

    a({
        id:28,
        title:"bugtrackingapplicationDataSet projectTableRowChangeEvent",
        content:"bugtrackingapplicationDataSet projectTableRowChangeEvent",
        description:'',
        tags:''
    });

    a({
        id:29,
        title:"bugtrackingapplicationDataSet projectTableRow",
        content:"bugtrackingapplicationDataSet projectTableRow",
        description:'',
        tags:''
    });

    a({
        id:30,
        title:"bugtrackingapplicationDataSet userTableRowChangeEventHandler",
        content:"bugtrackingapplicationDataSet userTableRowChangeEventHandler",
        description:'',
        tags:''
    });

    a({
        id:31,
        title:"ProjectStatus",
        content:"ProjectStatus",
        description:'',
        tags:''
    });

    a({
        id:32,
        title:"groupTableTableAdapter",
        content:"groupTableTableAdapter",
        description:'',
        tags:''
    });

    a({
        id:33,
        title:"AdminLogInForm",
        content:"AdminLogInForm",
        description:'',
        tags:''
    });

    a({
        id:34,
        title:"bugTableTableAdapter",
        content:"bugTableTableAdapter",
        description:'',
        tags:''
    });

    a({
        id:35,
        title:"AdminProjectTable",
        content:"AdminProjectTable",
        description:'',
        tags:''
    });

    a({
        id:36,
        title:"DeveloperTable",
        content:"DeveloperTable",
        description:'',
        tags:''
    });

    a({
        id:37,
        title:"bugtrackingapplicationDataSet bugTableRow",
        content:"bugtrackingapplicationDataSet bugTableRow",
        description:'',
        tags:''
    });

    a({
        id:38,
        title:"ReportBug",
        content:"ReportBug",
        description:'',
        tags:''
    });

    a({
        id:39,
        title:"UserInformation",
        content:"UserInformation",
        description:'',
        tags:''
    });

    a({
        id:40,
        title:"DeveloperInformation",
        content:"DeveloperInformation",
        description:'',
        tags:''
    });

    a({
        id:41,
        title:"bugtrackingapplicationDataSet groupTableRowChangeEventHandler",
        content:"bugtrackingapplicationDataSet groupTableRowChangeEventHandler",
        description:'',
        tags:''
    });

    a({
        id:42,
        title:"bugtrackingapplicationDataSet userTableRowChangeEvent",
        content:"bugtrackingapplicationDataSet userTableRowChangeEvent",
        description:'',
        tags:''
    });

    a({
        id:43,
        title:"ProjectTable",
        content:"ProjectTable",
        description:'',
        tags:''
    });

    a({
        id:44,
        title:"TesterTable",
        content:"TesterTable",
        description:'',
        tags:''
    });

    a({
        id:45,
        title:"Intro",
        content:"Intro",
        description:'',
        tags:''
    });

    a({
        id:46,
        title:"RegisterForm",
        content:"RegisterForm",
        description:'',
        tags:''
    });

    a({
        id:47,
        title:"LogInForm",
        content:"LogInForm",
        description:'',
        tags:''
    });

    a({
        id:48,
        title:"bugtrackingapplicationDataSet adminTableDataTable",
        content:"bugtrackingapplicationDataSet adminTableDataTable",
        description:'',
        tags:''
    });

    a({
        id:49,
        title:"projectTableTableAdapter",
        content:"projectTableTableAdapter",
        description:'',
        tags:''
    });

    a({
        id:50,
        title:"BugReport",
        content:"BugReport",
        description:'',
        tags:''
    });

    a({
        id:51,
        title:"ReportDeveloper",
        content:"ReportDeveloper",
        description:'',
        tags:''
    });

    a({
        id:52,
        title:"bugtrackingapplicationDataSet groupTableRowChangeEvent",
        content:"bugtrackingapplicationDataSet groupTableRowChangeEvent",
        description:'',
        tags:''
    });

    a({
        id:53,
        title:"ReportProject",
        content:"ReportProject",
        description:'',
        tags:''
    });

    a({
        id:54,
        title:"TesterInformation",
        content:"TesterInformation",
        description:'',
        tags:''
    });

    a({
        id:55,
        title:"bugtrackingapplicationDataSet adminTableRowChangeEventHandler",
        content:"bugtrackingapplicationDataSet adminTableRowChangeEventHandler",
        description:'',
        tags:''
    });

    a({
        id:56,
        title:"bugtrackingapplicationDataSet groupTableDataTable",
        content:"bugtrackingapplicationDataSet groupTableDataTable",
        description:'',
        tags:''
    });

    a({
        id:57,
        title:"bugtrackingapplicationDataSet groupTableRow",
        content:"bugtrackingapplicationDataSet groupTableRow",
        description:'',
        tags:''
    });

    y({
        url:'/api/BugTrackingApplication/DeveloperDashboard',
        title:"DeveloperDashboard",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/bugtrackingapplicationDataSet',
        title:"bugtrackingapplicationDataSet",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/bugTableRowChangeEventHandler',
        title:"bugtrackingapplicationDataSet.bugTableRowChangeEventHandler",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/AdminPanel',
        title:"AdminPanel",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/ManagerDashboard',
        title:"ManagerDashboard",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/adminTableRowChangeEvent',
        title:"bugtrackingapplicationDataSet.adminTableRowChangeEvent",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/projectTableDataTable',
        title:"bugtrackingapplicationDataSet.projectTableDataTable",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/UserChart',
        title:"UserChart",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/BugSolvedInformation',
        title:"BugSolvedInformation",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/HistoryInformation',
        title:"HistoryInformation",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication.bugtrackingapplicationDataSetTableAdapters/adminTableTableAdapter',
        title:"adminTableTableAdapter",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication.bugtrackingapplicationDataSetTableAdapters/userTableTableAdapter',
        title:"userTableTableAdapter",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/AdminChart',
        title:"AdminChart",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication.bugtrackingapplicationDataSetTableAdapters/TableAdapterManager',
        title:"TableAdapterManager",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/projectTableRowChangeEventHandler',
        title:"bugtrackingapplicationDataSet.projectTableRowChangeEventHandler",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/userTableDataTable',
        title:"bugtrackingapplicationDataSet.userTableDataTable",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/BugInformation',
        title:"BugInformation",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/BugStatus',
        title:"BugStatus",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/TesterDashboard',
        title:"TesterDashboard",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/bugTableRowChangeEvent',
        title:"bugtrackingapplicationDataSet.bugTableRowChangeEvent",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/bugTableDataTable',
        title:"bugtrackingapplicationDataSet.bugTableDataTable",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/userTableRow',
        title:"bugtrackingapplicationDataSet.userTableRow",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/UserTable',
        title:"UserTable",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/adminTableRow',
        title:"bugtrackingapplicationDataSet.adminTableRow",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/ReportTester',
        title:"ReportTester",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication.bugtrackingapplicationDataSetTableAdapters/UpdateOrderOption',
        title:"TableAdapterManager.UpdateOrderOption",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/RegexUtilities',
        title:"RegexUtilities",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/ProjectInformation',
        title:"ProjectInformation",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/projectTableRowChangeEvent',
        title:"bugtrackingapplicationDataSet.projectTableRowChangeEvent",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/projectTableRow',
        title:"bugtrackingapplicationDataSet.projectTableRow",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/userTableRowChangeEventHandler',
        title:"bugtrackingapplicationDataSet.userTableRowChangeEventHandler",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/ProjectStatus',
        title:"ProjectStatus",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication.bugtrackingapplicationDataSetTableAdapters/groupTableTableAdapter',
        title:"groupTableTableAdapter",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/AdminLogInForm',
        title:"AdminLogInForm",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication.bugtrackingapplicationDataSetTableAdapters/bugTableTableAdapter',
        title:"bugTableTableAdapter",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/AdminProjectTable',
        title:"AdminProjectTable",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/DeveloperTable',
        title:"DeveloperTable",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/bugTableRow',
        title:"bugtrackingapplicationDataSet.bugTableRow",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/ReportBug',
        title:"ReportBug",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/UserInformation',
        title:"UserInformation",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/DeveloperInformation',
        title:"DeveloperInformation",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/groupTableRowChangeEventHandler',
        title:"bugtrackingapplicationDataSet.groupTableRowChangeEventHandler",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/userTableRowChangeEvent',
        title:"bugtrackingapplicationDataSet.userTableRowChangeEvent",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/ProjectTable',
        title:"ProjectTable",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/TesterTable',
        title:"TesterTable",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/Intro',
        title:"Intro",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/RegisterForm',
        title:"RegisterForm",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/LogInForm',
        title:"LogInForm",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/adminTableDataTable',
        title:"bugtrackingapplicationDataSet.adminTableDataTable",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication.bugtrackingapplicationDataSetTableAdapters/projectTableTableAdapter',
        title:"projectTableTableAdapter",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/BugReport',
        title:"BugReport",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/ReportDeveloper',
        title:"ReportDeveloper",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/groupTableRowChangeEvent',
        title:"bugtrackingapplicationDataSet.groupTableRowChangeEvent",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/ReportProject',
        title:"ReportProject",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/TesterInformation',
        title:"TesterInformation",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/adminTableRowChangeEventHandler',
        title:"bugtrackingapplicationDataSet.adminTableRowChangeEventHandler",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/groupTableDataTable',
        title:"bugtrackingapplicationDataSet.groupTableDataTable",
        description:""
    });

    y({
        url:'/api/BugTrackingApplication/groupTableRow',
        title:"bugtrackingapplicationDataSet.groupTableRow",
        description:""
    });

    return {
        search: function(q) {
            return idx.search(q).map(function(i) {
                return idMap[i.ref];
            });
        }
    };
}();
