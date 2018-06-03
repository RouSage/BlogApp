$(document).ready(function () {

    $("#tablePosts").jqGrid({
        url: 'Posts',
        datatype: "json",
        mtype: 'GET',

        height: 'auto',

        // Table header name
        colNames: [
            'ID',
            'Title',
            'Description',
            'Content',
            'UrlSlug',
            'Published',
            'PostedOn',
            'Modified',
            'CategoryID',
            'Category',
            'Tags'
        ],
        // colModel takes the data from controller and binds to grid
        colModel: [
            {
                name: 'ID',
                index: 'ID',
                hidden: true,
                key: true
            },
            {
                name: 'Title',
                index: 'Title',
                width: 250,
                sortable: true
            },
            {
                name: 'Description',
                index: 'Description',
                hidden: true
            },
            {
                name: 'Content',
                index: 'Content',
                hidden: true
            },
            {
                name: 'UrlSlug',
                index: 'UrlSlug',
                width: 200,
                sortable: false
            },
            {
                name: 'Published',
                index: 'Published',
                width: 100,
                align: 'center'
            },
            {
                name: 'PostedOn',
                index: 'PostedOn',
                width: 150,
                align: 'center',
                sorttype: 'date',
                datefmt: 'm/d/Y'
            },
            {
                name: 'Modified',
                index: 'Modified',
                width: 100
            },
            {
                name: 'Category.ID',
                hidden: true
            },
            {
                name: 'Category.Name',
                index: 'Category',
                width: 200
            },
            {
                name: 'Tags',
                index: 'Tags',
                width: 250
            }
        ],

        // Pagination options
        toppager: true,
        pager: "#pagerPosts",
        rowNum: 10,
        rowList: [10, 20, 30],

        // Row number columns
        rownumbers: true,
        rownumWidth: 40,

        // Default sorting
        sortname: 'PostedOn',
        sortorder: "desc",

        // Display the no. of records message
        viewrecords: true,

        jsonReader: {
            repeatitems: false
        }
    });
});


    //$("#tabs").tabs({
    //    show: function (event, ui) {

    //        if (!ui.tab.isLoaded) {

    //            var gdMgr = BlogApp.GridManager,
    //                fn, gridName, pagerName;

    //            switch (ui.index) {
    //                case 0:
    //                    fn = gdMgr.postsGrid;
    //                    gridName = "#tablePosts";
    //                    pagerName = "#pagerPosts";
    //                    break;
    //                case 1:
    //                    fn = gdMgr.categoriesGrid;
    //                    gridName = "#tableCategories";
    //                    pagerName = "#pagerCategories";
    //                    break;
    //                case 2:
    //                    fn = gdMgr.tagsGrid;
    //                    gridName = "#tableTags";
    //                    pagerName = "#pagerTags";
    //                    break;
    //            };

    //            fn(gridName, pagerName);
    //            ui.tab.isLoaded = true;
    //        }
    //    }
    //});