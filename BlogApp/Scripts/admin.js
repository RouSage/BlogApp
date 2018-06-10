$(document).ready(function () {

    var BlogApp = {};

    BlogApp.GridManager = {};
    
    // POSTS GRID
    BlogApp.GridManager.postsGrid = $("#tablePosts").jqGrid({
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
                sortable: true,
                editable: true,
                editoptions: {
                    size: 43,
                    maxlength: 500
                },
                editrules: {
                    required: true
                }
            },
            {
                name: 'Description',
                index: 'Description',
                width: 250,
                sortable: false,
                hidden: true,
                editable: true,
                edittype: 'textarea',
                editoptions: {
                    rows: "10",
                    cols: "100"
                },
                editrules: {
                    // Custom validation needed because of tinyMCE editor
                    custom: true,
                    custom_func: function (val, colname) {
                        val = tinymce.get("Description").getContent();
                        if (val) return [true, ""];
                        return [false, colname + ": Field is required"];
                    },
                    edithidden: true
                }
            },
            {
                name: 'Content',
                index: 'Content',
                width: 250,
                sortable: false,
                hidden: true,
                editable: true,
                edittype: 'textarea',
                editoptions: {
                    rows: "40",
                    cols: "100"
                },
                editrules: {
                    // Custom validation needed because of tinyMCE editor
                    custom: true,
                    custom_func: function (val, colname) {
                        val = tinymce.get("Content").getContent();
                        if (val) return [true, ""];
                        return [false, colname + ": Field is required"];
                    },
                    edithidden: true
                }
            },
            {
                name: 'UrlSlug',
                index: 'UrlSlug',
                width: 200,
                sortable: false,
                editable: true,
                editoptions: {
                    size: 43,
                    maxlength: 50
                },
                editrules: {
                    required: true
                }
            },
            {
                name: 'Published',
                index: 'Published',
                width: 100,
                align: 'center',
                editable: true,
                edittype: 'checkbox',
                editoptions: {
                    value: "true:false",
                    defaultValue: 'false'
                }
            },
            {
                name: 'PostedOn',
                index: 'PostedOn',
                width: 150,
                align: 'center',
                sorttype: 'date',
                datefmt: 'm.d.Y'
            },
            {
                name: 'Modified',
                index: 'Modified',
                width: 150,
                align: 'center',
                sorttype: 'date',
                datefmt: 'm.d.Y'
            },
            {
                name: 'Category.ID',
                hidden: true,
                editable: true,
                edittype: 'select',
                editoptions: {
                    style: 'width: 250px;',
                    dataUrl: 'GetCategoriesHtml'
                },
                editrules: {
                    required: true,
                    edithidden: true
                }
            },
            {
                name: 'Category.Name',
                index: 'Category',
                width: 150
            },
            {
                name: 'Tags',
                index: 'Tags',
                width: 150,
                editable: true,
                edittype: 'select',
                editoptions: {
                    style: 'width: 250px;',
                    dataUrl: 'GetTagsHtml',
                    multiple: true
                },
                editrules: {
                    required: true
                }
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

        caption: "Posts",

        jsonReader: {
            repeatitems: false
        },

        // Show tags as string and not as [object Object]
        afterInsertRow: function (rowid, rowdata, rowelem) {
            var tags = rowdata["Tags"];
            var tagStr = "";

            $.each(tags, function (i, t) {
                if (tagStr) tagStr += ", ";
                tagStr += t.Name;
            });

            $(this).setRowData(rowid, { "Tags": tagStr });
        }

    });

    var afterShowFormHandler = function () {
        tinymce.init({
            selector: '#Description',
            theme: 'modern',
            branding: false,
            resize: false,
            statusbar: false,
            menubar: false,
            browser_spellcheck: true,
            contextmenu: false,
            plugins: 'lists advlist link anchor image searchreplace help code hr visualblocks visualchars table textcolor charmap',
            toolbar: [
                'bold italic underline strikethrough code | alignleft aligncenter alignright alignjustify | visualblocks visualchars removeformat | styleselect formatselect',
                'undo redo | forecolor backcolor | bullist numlist | indent outdent | link unlink | anchor searchreplace help',
                'fontsizeselect | hr table image charmap | subscript superscript'
            ],
            // Link plugin options
            link_context_toolbar: true,
            link_title: false,
            // Code plugin options
            code_dialog_height: 300,
            code_dialog_width: 600
        });
        tinymce.init({
            selector: "#Content",
            theme: 'modern',
            branding: false,
            resize: false,
            statusbar: false,
            menubar: false,
            browser_spellcheck: true,
            contextmenu: false,
            plugins: 'lists advlist link anchor image searchreplace help code hr visualblocks visualchars table textcolor charmap',
            toolbar: [
                'bold italic underline strikethrough code | alignleft aligncenter alignright alignjustify | visualblocks visualchars removeformat | styleselect formatselect',
                'undo redo | forecolor backcolor | bullist numlist | indent outdent | link unlink | anchor searchreplace help',
                'fontsizeselect | hr table image charmap | subscript superscript'
            ],
            // Link plugin options
            link_context_toolbar: true,
            link_title: false,
            // Code plugin options
            code_dialog_height: 300,
            code_dialog_width: 600
        });
    };

    // Prevents dialog closing even though the server returns and error
    var afterSubmitHandler = function (response, postdata) {
        var json = $.parseJSON(response.responseText);

        if (json) return [json.success, json.message, json.id];

        return [false, "Failed to get result from server.", null];
    };

    var beforeSubmitHandler = function (postdata, form) {
        var selRowData = $('#tablePosts').getRowData($('#tablePosts').getGridParam('selrow'));
        if (selRowData["PostedOn"])
            postdata.PostedOn = selRowData["PostedOn"];
        postdata.Description = tinymce.get("Description").getContent();
        postdata.Content = tinymce.get("Content").getContent();

        return [true];
    };

    var onCloseHandler = function () {
        tinymce.remove('#Description');
        tinymce.remove('#Content');
    };

    BlogApp.GridManager.postsNavGrid = $("#tablePosts").navGrid("#pagerPosts",
        {   // Parameters
            cloneToTop: true,
            search: false,
            addtitle: 'Add new post',
            edittitle: 'Edit selected post',
            deltitle: 'Delete selected post',
            alerttext: 'Please, select row'
        },
        {   // Edit options   
            url: 'EditPost',
            editCaption: 'Edit Post',
            processData: "Saving...",
            width: 1000,
            closeAfterEdit: true,
            closeOnEscape: true,
            //afterclickPgButtons:
            // Initialize all text editors after form was showed
            afterShowForm: afterShowFormHandler,
            // Remove all text editors after form was closed
            onClose: onCloseHandler,
            // Read values from the editors and add them to the post data
            // so the successfully submotted to the server
            beforeSubmit: beforeSubmitHandler,
            afterSubmit: afterSubmitHandler
        },
        {    // Add options
            url: 'AddPost',
            addCaption: 'Add Post',
            processData: "Saving...",
            width: 1000,
            closeAfterAdd: true,
            closeOnEscape: true,
            // Initialize all text editors after form was showed
            afterShowForm: afterShowFormHandler,
            // Remove all text editors after form was closed
            onClose: onCloseHandler,
            // Read values from the editors and add them to the post data
            // so the successfully submotted to the server
            beforeSubmit: beforeSubmitHandler,
            // Prevents dialog closing even though the server returns and error
            afterSubmit: afterSubmitHandler
        },
        {   // Delete options
            url: 'DeletePost',
            caption: "Delete Post",
            processData: "Saving...",
            msg: "Delete the Post?",
            closeOnEscape: true,
            afterSubmit: afterSubmitHandler
        }
    );

    // CATEGORIES GRID
    BlogApp.GridManager.categoriesGrid = $("#tableCategories").jqGrid({
        url: 'Categories',
        datatype: "json",
        mtype: 'GET',

        height: 'auto',

        // Table header names
        colNames: [
            'ID',
            'Name',
            'UrlSlug',
            'Frequence'
        ],
        // colModel takes the data from controller and binds to grid
        colModel: [
            {
                name: 'ID',
                index: 'ID',
                hidden: true,
                sorttype: 'int',
                key: true,
                editable: false,
                editoptions: {
                    readonly: true
                }
            },
            {
                name: 'Name',
                index: 'Name',
                width: 200,
                sortable: true,
                editable: true,
                edittype: 'text',
                editoptions: {
                    size: 30,
                    maxlength: 500
                },
                editrules: {
                    required: true
                }
            },
            {
                name: 'UrlSlug',
                index: 'UrlSlug',
                width: 200,
                sortable: false,
                editable: true,
                edittype: 'text',
                editoptions: {
                    size: 30,
                    maxlength: 500
                },
                editrules: {
                    required: true
                }
            },
            {
                name: 'Frequence',
                index: 'Frequence',
                width: 100,
                sortable: true,
                editable: false,
                edittype: 'int',
                editoptions: {
                    readonly: true
                }
            }
        ],

        // Pagination options
        toppager: true,
        pager: "#pagerCategories",

        // Row number columns
        rownumbers: true,
        rownumWidth: 40,
        rowNum: 500,

        // Default sorting
        sortname: 'Name',

        // Display the no. of records message
        viewrecords: true,

        caption: "Categories",

        loadonce: true,

        jsonReader: {
            repeatitems: false
        }

    });

    BlogApp.GridManager.categoriesNavGrid = $("#tableCategories").navGrid("#pagerCategories",
        {   // Parameters
            cloneToTop: true,
            search: false,
            addtitle: 'Add new category',
            edittitle: 'Edit selected category',
            deltitle: 'Delete selected category',
            alerttext: 'Please, select row'
        },
        {   // Edit options   
            url: 'EditCategory',
            width: 400,
            editCaption: 'EditCategory',
            processData: "Saving...",
            closeAfterEdit: true,
            closeOnEscape: true,
            afterSubmit: function (response, postdata) {
                var json = $.parseJSON(response.responseText);

                if (json) {
                    $("#tableCategories").jqGrid('setGridParam', { datatype: 'json' });
                    return [json.success, json.message, json.id];
                }

                return [false, "Failed to get result from server.", null];
            }
        },
        {   // Add options
            url: 'AddCategory',
            width: 400,
            addCaption: 'Add Category',
            processData: "Saving...",
            closeAfterAdd: true,
            closeOnEscape: true,
            afterSubmit: function (response, postdata) {
                var json = $.parseJSON(response.responseText);

                if (json) {
                    // Since the data is in the cliend-side, reload the grid
                    $("#tableCategories").jqGrid('setGridParam', { datatype: 'json' });
                    return [json.success, json.message, json.id];
                }

                return [false, "Failed to get result from server.", null];
            }
        },
        {   // Delete options
            url: 'DeleteCategory',
            caption: 'Delete Category',
            processData: "Saving...",
            width: 450,
            msg: "Delete the category? This will delete all the posts belongs to this category as well.",
            closeOnEscape: true,
            afterSubmit: afterSubmitHandler
        }
    );

    // TAGS GRID
    BlogApp.GridManager.tagsGrid = $("#tableTags").jqGrid({
        url: 'Tags',
        datatype: "json",
        mtype: 'GET',

        height: 'auto',

        // Table header names
        colNames: [
            'ID',
            'Name',
            'UrlSlug'
        ],
        // colModel takes the data from controller and binds to grid
        colModel: [
            {
                name: 'ID',
                index: 'ID',
                hidden: true,
                sorttype: 'int',
                key: true,
                editable: false,
                editoptions: {
                    readonly: true
                }
            },
            {
                name: 'Name',
                index: 'Name',
                width: 300,
                sortable: true,
                editable: true,
                edittype: 'text',
                editoptions: {
                    size: 30,
                    maxlength: 50
                },
                editrules: {
                    required: true
                }
            },
            {
                name: 'UrlSlug',
                index: 'UrlSlug',
                width: 200,
                sortable: false,
                editable: true,
                edittype: 'text',
                editoptions: {
                    size: 30,
                    maxlength: 50
                },
                editrules: {
                    required: true
                }
            }
        ],

        // Pagination options
        toppager: true,
        pager: "#pagerTags",

        // Row number columns
        rownumbers: true,
        rownumWidth: 40,
        rowNum: 500,

        // Default sorting
        sortname: 'Name',

        // Display the no. of records message
        viewrecords: true,

        caption: "Tags",

        loadonce: true,

        jsonReader: {
            repeatitems: false
        }

    });

    BlogApp.GridManager.tagsNavGrid = $("#tableTags").navGrid("#pagerTags",
        {   // Parameters
            cloneToTop: true,
            search: false,
            addtitle: 'Add new tag',
            edittitle: 'Edit selected tag',
            deltitle: 'Delete selected tag',
            alerttext: 'Please, select row'
        },
        {   // Edit options   

        },
        {   // Add options
            url: 'AddTag',
            width: 400,
            addCaption: 'Add Tag',
            processData: "Saving...",
            closeAfterAdd: true,
            closeOnEscape: true,
            afterSubmit: function (response, postdata) {
                var json = $.parseJSON(response.responseText);

                if (json) {
                    $("#tableTags").jqGrid('setGridParam', { datatype: 'json' });
                    return [json.success, json.message];
                }

                return [false, "Failed to get result from server.", null];
            }
        },
        {   // Delete options

        }
    );

});