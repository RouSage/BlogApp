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
                edittype: 'textarea',
                editoptions: {
                    rows: "10",
                    cols: "100"
                },
                editrules: {
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
                width: 100,
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
                if (tagStr) tagStr += ", "
                tagStr += t.Name;
            });

            $(this).setRowData(rowid, { "Tags": tagStr });
        }

    });

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

        },
        {    // Add options
            url: 'AddPost',
            addCaption: 'Add Post',
            processData: "Saving...",
            width: 1000,
            closeAfterAdd: true,
            closeOnEscape: true,
            afterShowForm: function () {
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
                        'fontsizeselect | hr table image charmap | subscript superscript',
                    ],
                    // Link plugin options
                    link_context_toolbar: true,
                    link_title: false,
                    // Code plugin options
                    code_dialog_height: 300,
                    code_dialog_width: 600,
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
                        'fontsizeselect | hr table image charmap | subscript superscript',
                    ],
                    // Link plugin options
                    link_context_toolbar: true,
                    link_title: false,
                    // Code plugin options
                    code_dialog_height: 300,
                    code_dialog_width: 600,
                });
            },
            onClose: function () {
                tinymce.remove('#Description');
                tinymce.remove('#Content');
            }
        },
        {   // Delete options
            
        }
    );

});