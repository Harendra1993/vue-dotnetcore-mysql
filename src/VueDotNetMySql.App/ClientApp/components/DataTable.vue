<template>
  <div :class="classes">
    <table
      v-once
      :id="tableId"
      ref="table"
      :class="className"
      cellpadding="0"
      style="width:100%"
    >
      <thead>
        <tr>
          <th
            v-for="(field, i) in options.columns"
            :key="i"
            :class="field.className"
          >
            <slot :name="`HEAD_${field.name}`" :field="field" :i="i">
              <div v-html="field.title" />
            </slot>
          </th>
        </tr>
      </thead>
    </table>
  </div>
</template>

<script>
import Vue from "vue";
import "pdfmake";
import "datatables.net-bs";
import "datatables.net-buttons-bs";
import "datatables.net-buttons/js/buttons.colVis.js";
import "datatables.net-buttons/js/buttons.flash.js";
import "datatables.net-buttons/js/buttons.html5.js";
import "datatables.net-buttons/js/buttons.print.js";
import "datatables.net-colreorder-bs";
import "datatables.net-fixedcolumns-bs";
import "datatables.net-fixedheader-bs";
import "datatables.net-keytable-bs";
import "datatables.net-responsive-bs";
import "datatables.net-rowgroup-bs";
import "datatables.net-rowreorder-bs";
import "datatables.net-scroller-bs";
import "datatables.net-select-bs";
let myUniqueId = 1;

export default {
  name: "DataTable",
  props: {
    /**
     * The table id
     *
     * @type String
     */
    id: {
      type: String,
      default: null
    },
    /**
     * Set the container classes.
     *
     * @type String
     */
    containerClassName: {
      type: String,
      default: "table-responsive d-print-inline"
    },
    /**
     * Set the table classes you wish to use, default with bootstrap4
     * but you can override with: themeforest, foundation, etc..
     *
     * @type String
     */
    className: {
      type: String,
      default: "table table-hover table-condensed wrap"
    },
    /**
     * the options object: https://datatables.net/manual/options
     *
     * @type Object
     */
    opts: {
      type: Object
    },
    /**
     * List all fields to be converted to opts columns
     *
     * @type Object
     */
    fields: {
      type: Object
    },
    /**
     * Pass in DataTables.Net jQuery to resolve any conflict from
     * multiple jQuery loaded in the browser
     *
     * @type Object
     */
    jquery: {
      type: Object
    },
    /**
     * The select-checkbox column index (start at 1)
     * Current implementation require datatables.net-select
     *
     * @type Number
     */
    selectCheckbox: {
      type: Number
    },
    /**
     * Provide custom local data loading.  Warning: this option has not been
     * thoroughly tested.  Please use ajax and serverSide instead.
     *
     * @type Function
     */
    dataLoader: {
      type: Function
    },
    /**
     * true to hide the footer of the table
     *
     * @type Boolean
     */
    hideFooter: {
      type: Boolean
    },
    /**
     * The details column configuration of master/details.
     *
     * @type {Object}
     */
    details: {
      type: Object
    },
    /**
     * The details column configuration of master/details.
     *
     * @type {Object}
     */
    rowClick: {
      type: Object
    }
  },
  data() {
    // initialize defaults
    return {
      tableId: null,
      options: {
        /*eslint-disable */
        dom: "<'html5buttons'B>lTfgirtp",
        /*eslint-enable */
        mark: true,
        responsive: true,
        columns: [],
        language: {
          sEmptyTable:
            "<div class='col-sm-12'><div class='img-responsive text-center mt-10'><img src='/img/no-record.png' style='height:300px; width:700px;'></div></div>",
          sInfo: "Showing _START_ to _END_ of _TOTAL_ entries",
          sInfoEmpty: "Showing 0 to 0 of 0 entries",
          sInfoFiltered: "(filtered from _MAX_ total entries)",
          sInfoPostFix: "",
          sInfoThousands: ",",
          sLengthMenu: "Show _MENU_ entries",
          loadingRecords:
            "<div class='spiner-example'><div class='sk-spinner sk-spinner-pulse'></div></div>",
          Processing:
            "<div class='spiner-example'><div class='sk-spinner sk-spinner-pulse'></div></div>",
          sSearch: "Filter:",
          zeroRecords:
            "<div class='col-sm-12'><div class='img-responsive text-center mt-10'><img src='/img/no-record.png' style='height:300px; width:700px;'></div></div>",
          oPaginate: {
            sFirst: "First",
            sLast: "Last",
            sNext: "Next",
            sPrevious: "Previous"
          }
        },

        lengthMenu: [
          [15, 100, 500, 1000, -1],
          [15, 100, 500, 1000, "All"]
        ],
        pageLength: 15,
        buttons: this.getDefaultButtons() // remove any button defaults
      },
      dataTable: null,
      dt: this
    };
  },
  computed: {
    jq() {
      return this.jquery || window.jQuery;
    },
    classes() {
      const that = this;
      let classes = `${that.containerClassName} dt-container`;
      if (this.hideFooter) {
        classes += " hide-footer";
      }

      return classes;
    }
  },
  created() {
    const vm = this;
    const jq = vm.jq;

    vm.tableId = vm.id || `dttable${myUniqueId++}`;

    // allow user to override default options
    if (vm.opts) {
      vm.options = jq.extend({}, vm.options, vm.opts);
    }
  },
  mounted() {
    const vm = this;
    const jq = vm.jq;
    let $el = jq(vm.$refs.table);
    let orders = [];

    let startCol = 0;
    let icol = 0;

    // if fields are passed in, generate column definition
    // from our custom fields schema
    if (vm.fields) {
      const fields = vm.fields;
      const cols = vm.options.columns;

      for (let k in fields) {
        const field = fields[k];
        field.name = field.name || k;

        // disable search and sort for local field
        if (field.isLocal) {
          field.searchable = false;
          field.sortable = false;
        }

        // generate
        let col = {
          title: field.label || field.name,
          data: field.data || field.name,
          width: field.width,
          name: field.name,
          className: field.className
        };

        if (field.width) {
          col.width = field.width;
        }

        if (field.hasOwnProperty("defaultContent")) {
          col.defaultContent = field.defaultContent;
        }

        if (field.hasOwnProperty("sortable")) {
          col.orderable = field.sortable;
        }

        if (field.hasOwnProperty("visible")) {
          col.visible = field.visible;
        }

        if (field.hasOwnProperty("searchable")) {
          col.searchable = field.searchable;
        }

        if (field.template || vm.$scopedSlots[field.name]) {
          field.render = vm.compileTemplate(field, vm.$scopedSlots[field.name]);
        }

        if (field.render) {
          if (!field.render.templated) {
            let myRender = field.render;
            field.render = function() {
              return myRender.apply(vm, Array.prototype.slice.call(arguments));
            };
          }

          col.render = field.render;
        }

        // console.log(col)
        cols.push(col);

        if (field.defaultOrder) {
          orders.push([icol, field.defaultOrder]);
        }

        icol++;
      }
    }

    // apply orders calculated from above
    vm.options.order = vm.options.order || orders;

    if (vm.selectCheckbox) {
      vm.selectCheckbox = vm.selectCheckbox || 1;

      // create checkbox column
      const col = {
        orderable: false,
        searchable: false,
        name: "_select_checkbox",
        className: "select-checkbox d-print-none",
        data: null,
        defaultContent: "",
        title:
          '<input type="checkbox" class="select-all-checkbox d-print-none">'
      };
      vm.options.columns.splice(vm.selectCheckbox - 1, 0, col);

      // console.log(vm.options.columns)
      vm.options.select = jq.extend(vm.options.select || {}, {
        style: "os",
        selector: "td.select-checkbox"
      });

      if (vm.selectCheckbox === 1) {
        startCol++;
      }
    }

    // handle master details
    if (vm.details) {
      vm.details.index = vm.details.index || 1;

      // create details column
      const col = {
        orderable: false,
        searchable: false,
        name: "_details_control",
        className: "details-control d-print-none",
        data: null,
        defaultContent:
          vm.details.icons ||
          '<span class="details-plus" title="Show details"></span><span class="details-minus" title="Hide details"></span>'
      };
      vm.options.columns.splice(vm.details.index - 1, 0, col);

      if (vm.details.index === 1) {
        startCol++;
      }
    }

    if (startCol > 0) {
      if (vm.options.order) {
        vm.options.order.forEach(v => {
          v[0] += startCol;
        });
      } else {
        vm.options.order = [[startCol, "asc"]];
      }
    }

    // handle local data loader
    if (vm.dataLoader) {
      delete vm.options.ajax;
      vm.options.serverSide = false;
    }
    vm.$emit("before-creating", true);

    vm.options.drawCallback = function(settings) {
      //console.log(Math.ceil((this.fnSettings().fnRecordsDisplay()) / this.fnSettings()._iDisplayLength));
      if (
        Math.ceil(
          this.fnSettings().fnRecordsDisplay() /
            this.fnSettings()._iDisplayLength
        ) > 1
      ) {
        jq("#" + vm.tableId + "_paginate").css("display", "block");
      } else {
        jq("#" + vm.tableId + "_paginate").css("display", "none");
      }
    };

    // you can access and update the vm.options and $el here before we create the DataTable
    vm.$emit("table-creating", vm, $el);

    vm.dataTable = $el.DataTable(vm.options);
    if (vm.selectCheckbox) {
      // handle select all checkbox
      $el.on("click", "th input.select-all-checkbox", e => {
        if (jq(e.target).is(":checked")) {
          vm.dataTable.rows().select();
        } else {
          vm.dataTable.rows().deselect();
        }
      });

      // handle individual row select events
      vm.dataTable.on("select deselect", () => {
        const $input = $el.find("th input.select-all-checkbox");
        if (
          vm.dataTable
            .rows({
              selected: true
            })
            .count() !== vm.dataTable.rows().count()
        ) {
          jq("th.select-checkbox").removeClass("selected");
          $input.attr("checked", false);
        } else {
          jq("th.select-checkbox").addClass("selected");
          $input.attr("checked", true);
        }
        // TODO: vm.$emit the selected row?
      });
    }

    if (vm.rowClick) {
      const condition = vm.rowClick.condition ? vm.rowClick.condition : "tr";

      jq("#" + vm.tableId).css("cursor", "pointer");

      jq("#" + vm.tableId + " tbody").on("click", condition, e => {
        e.preventDefault();
        e.stopPropagation();
        const target = jq(e.target);
        let that = target;
        let tr = that.closest("tr");
        if (tr) {
          if (tr.attr("role") !== "row") {
            tr = tr.prev();
          }
          const row = vm.dataTable.row(tr);
          const data = row.data();

          // if (tr.hasClass("selected")) {
          //   tr.removeClass("selected");
          // } else {
          //   vm.dataTable.$("tr.selected").removeClass("selected");
          //   tr.addClass("selected");
          // }
          vm.$emit("rowClick", data, row, tr, that);
        }
      });
    }

    // wire up edit, delete, and/or action buttons
    $el.on("click", "[data-action]", e => {
      e.preventDefault();
      e.stopPropagation();
      const target = jq(e.target);
      let that = target;
      let action = that.attr("data-action");
      while (!action) {
        // don't let it propagate outside of container
        if (that.hasClass("dt-container") || that.prop("tagName") === "table") {
          // no action, simply exit
          return;
        }
        that = that.parent();
        action = that.attr("data-action");
      }

      // only emit if there is action
      if (action) {
        // detect if row action
        let tr = that.closest("tr");
        if (tr) {
          if (tr.attr("role") !== "row") {
            tr = tr.prev();
          }
          const row = vm.dataTable.row(tr);
          const data = row.data();
          vm.$emit(action, data, row, tr, that);
        } else {
          // not a row click, must be other kind of action
          // such as bulk, csv, pdf, etc...
          vm.$emit(action, null, null, null, target);
        }
      }
    });

    // handle master/details
    if (vm.details) {
      // default to render function
      let renderFunc = vm.details.render;
      // must be string template
      if (vm.details.template || vm.$scopedSlots["_details"]) {
        renderFunc = vm.compileTemplate(
          vm.details,
          vm.$scopedSlots["_details"]
        );
      } else if (renderFunc) {
        renderFunc = function() {
          return vm.details.render.apply(
            vm,
            Array.prototype.slice.call(arguments)
          );
        };
      }

      // handle master/details
      // Add event listener for opening and closing details
      if (vm.details.template && vm.details.template != "") {
        $el.on("click", "td.details-control", e => {
          e.preventDefault();
          e.stopPropagation();
          const target = jq(e.target);
          let that = target;
          let tr = that.closest("tr");
          if (tr.attr("role") !== "row") {
            tr = tr.prev();
          }
          const row = vm.dataTable.row(tr);
          if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
            tr.removeClass("shown");
          } else {
            // Open this row
            const data = row.data();
            row.child(renderFunc(data, "child", row, tr)).show();

            tr.addClass("shown");
          }
        });
      }
    }

    vm.$emit("table-created", vm);

    // finally, load data
    if (vm.dataLoader) {
      vm.reload();
    }
  },
  beforeDestroy() {
    const vm = this;
    if (vm.dataTable) {
      vm.dataTable.destroy(true);
    }

    vm.dataTable = null;
  },
  methods: {
    dtExpandToggle(config) {
      if (config.row.child.isShown()) {
        // This row is already open - close it
        config.row.child.hide();
        config.tr.removeClass("selected-row");
        config.tr.removeClass("shown");
      } else {
        // Open this row
        config.row.child(this.format(config)).show();
        config.row
          .child()
          .css(
            "background-color",
            $(config.row.node()).css("background-color")
          );
        config.tr.addClass("selected-row");
        config.tr.addClass("shown");
      }
    },
    format(config) {
      this.populateDtFullFormatable(config);
      return this.generateTable(config.dt, config.headers);
    },
    populateDtFullFormatable(config) {
      var vm = this;
      $.post(config.url, config.post).done(function(data) {
        var table = $(config.dt).DataTable({
          processing: true,
          searching: false,
          lengthChange: false,
          serverSide: false,
          ordering: true,
          pageLength: 5,
          lengthMenu: [5, 10, 15, 25, 50, 100],
          mark: true,
          responsive: true,
          columns: config.columns,
          initComplete: function(settings, json) {
            $(
              this.api()
                .table()
                .header()
            ).css({ "background-color": "#bb8b30", color: "#fff" });
          },
          drawCallback: function(settings) {
            //console.log(Math.ceil((this.fnSettings().fnRecordsDisplay()) / this.fnSettings()._iDisplayLength));
            if (
              Math.ceil(
                this.fnSettings().fnRecordsDisplay() /
                  this.fnSettings()._iDisplayLength
              ) > 1
            ) {
              $(config.dt + "_paginate").css("display", "block");
            } else {
              $(config.dt + "_paginate").css("display", "none");
            }
          }
        });
        table.clear().draw();
        table.rows.add(data).draw();
        vm.details.dataTable;
      });
    },
    generateTable(id, headers) {
      id = id.replace(/#/g, "");
      let html = "<div>";
      html += '<table id="' + id + '" class="table" >';
      html += "<thead><tr>";
      for (var i = 0; i < headers.length; i++) {
        html += "<th>" + headers[i] + "</th>";
      }
      html += "</tr>";
      html += "</thead></table>";
      html += "</div>";
      return html;
    },

    /**
     * Vue.compile a template string and return the compiled function
     *
     * @param  {Object} object with template property
     * @param  {Object} the slot
     * @return {Function}          the compiled template function
     */
    compileTemplate(field, slot) {
      const vm = this;
      const jq = vm.jq;
      const res = Vue.compile(`<div>${field.template || ""}</div>`);

      const renderFunc = (data, type, row, meta) => {
        let myRender = res.render;
        if (slot) {
          myRender = createElement => {
            return createElement("div", [
              slot({
                data: data,
                type: type,
                row: row,
                meta: meta,
                dt: vm,
                def: field,
                comp: vm.$parent
              })
            ]);
          };
        }

        const comp = new Vue({
          data: {
            data: data,
            type: type,
            row: row,
            meta: meta,
            dt: vm,
            def: field,
            comp: vm.$parent
          },
          render: myRender,
          staticRenderFns: res.staticRenderFns
        }).$mount();
        return jq(comp.$el).html();
      };

      renderFunc.templated = true;

      return renderFunc;
    },
    /**
     * Set table data array that was loaded from somewhere else
     * This method allow for local setting of data; though, it
     * is recommended to use ajax instead of this.
     *
     * @param {Array} data   the array of data
     * @return {Object}            the component
     */
    setTableData(data) {
      const vm = this;
      if (data.constructor === Array) {
        vm.dataTable.clear().draw();
        if (vm.dataTable) vm.dataTable.rows.add(data);
        vm.dataTable.draw(false);
        // vm.dataTable.columns.adjust();
        vm.dataTable.columns.adjust().draw();
      }
      return vm;
    },

    /**
     * Set table data object that was loaded from somewhere else
     * This method allow for local setting of data; though, it
     * is recommended to use ajax instead of this.
     *
     * @param {Object} data   the object of data
     * @return {Object}            the component
     */
    addTableRow(data) {
      const vm = this;
      if (data.constructor === Object) {
        if (vm.dataTable) vm.dataTable.row.add(data);
        vm.dataTable.draw(false);
        // vm.dataTable.columns.adjust();
        vm.dataTable.columns.adjust().draw();
      }
      return vm;
    },

    /**
     * Set table data object that was loaded from somewhere else
     * This method allow for local setting of data; though, it
     * is recommended to use ajax instead of this.
     *
     * @param {Object} data   the object of data
     * @return {Object}            the component
     */
    updateTableRow(data, rowId) {
      const vm = this;
      if (data.constructor === Object) {
        if (vm.dataTable)
          vm.dataTable
            .row(rowId)
            .data(data)
            .draw(false);
      }
      return vm;
    },

    /**
     * pass through reload method
     *
     * @param  {Boolean}  resetPaging true to reset current page position
     * @return {Object}            the component
     */
    reload(resetPaging = false) {
      const vm = this;
      if (vm.dataLoader) {
        // manual data loading
        vm.dataLoader(data => {
          if (data && !data.data) {
            data = { data: data };
          }
          vm.setTableData(data.data);

          vm.$emit("reloaded", data, vm);
        });
      } else {
        vm.dataTable.ajax.reload(data => {
          vm.$emit("reloaded", data, vm);
        }, resetPaging);
      }

      return vm;
    },
    search(value) {
      const vm = this;
      vm.dataTable.search(value).draw();

      return vm;
    },
    setPageLength(value) {
      const vm = this;
      vm.dataTable.page.len(value);

      return vm.reload();
    },
    getServerParams() {
      if (this.dataLoader) {
        return {};
      }

      return this.dataTable.ajax.params();
    },
    getDefaultButtons() {
      if (this.opts.exports && this.opts.exports.hasOwnProperty("title")) {
        return [
          { extend: "copy", exportOptions: { modifier: { page: "current" } } },
          {
            extend: "csv",
            title: this.opts.exports.title + this.now(),
            exportOptions: { modifier: { page: "current" } }
          },
          {
            extend: "excel",
            title: this.opts.exports.title + this.now(),
            exportOptions: { modifier: { page: "current" } }
          },
          {
            extend: "pdf",
            title: this.opts.exports.title + this.now(),
            exportOptions: { modifier: { page: "current" } }
          },
          {
            extend: "print",
            exportOptions: { modifier: { page: "current" } },
            customize: function(win) {
              $(win.document.body).addClass("white-bg");
              $(win.document.body).css("font-size", "10px");
              $(win.document.body)
                .find("table")
                .addClass("compact")
                .css("font-size", "inherit");
            }
          }
        ];
      }

      return [];
    },
    showColumns(table, cols = []) {
      var column = table.columns(cols);
      // Toggle the visibility
      column.visible(true);
    },
    hideColumns(table, cols = []) {
      var column = table.columns(cols);
      // Toggle the visibility
      column.visible(false);
    },
    now() {
      var date = new Date();
      var yyyy = date.getFullYear();
      var dd = date.getDate();
      var mm = date.getMonth() + 1;

      if (dd < 10) dd = "0" + dd;

      if (mm < 10) mm = "0" + mm;

      var cur_day = dd + "-" + mm + "-" + yyyy;

      var hours = date.getHours();
      var minutes = date.getMinutes();
      var seconds = date.getSeconds();

      if (hours < 10) hours = "0" + hours;

      if (minutes < 10) minutes = "0" + minutes;

      if (seconds < 10) seconds = "0" + seconds;

      return cur_day + " " + hours + ":" + minutes + ":" + seconds;
    }
  }
};
</script>
<style>
/* .table tbody tr:hover {
  color: #000;
  font-weight: bold;
} */
.select-checkbox,
.select-all-checkbox {
  cursor: pointer;
}
.dt-footer .dataTables_length {
  padding-top: 6px;
  padding-right: 10px;
}
.dt-footer .dataTables_length,
.dt-footer .dataTables_paginate {
  float: right;
}
.hide-footer .dt-footer {
  display: none;
}

.details-control {
  cursor: pointer;
  font-weight: 700;
}
</style>
