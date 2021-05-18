<template>
  <div>
    <TheNavbar />
    <TheHeader />
    <TheContent />
  </div>
</template>

<script>
//#region import
import TheNavbar from "./components/layout/TheNavbar.vue";
import TheHeader from "./components/layout/TheHeader.vue";
import TheContent from "./components/layout/TheContent.vue";
//#endregion
//#region export
export default {
  //#region components
  components: {
    TheNavbar,
    TheHeader,
    TheContent,
  },
  //#endregion
};
//#endregion
</script>

<style lang="scss">
//Stylesheet chung.
@font-face {
  font-family: NotoSans;
  src: url("./assets/fonts/notosans-regular.2cb88a13.woff2") format("woff2");
}

// Biến chung.
$navbar-width: 178px;

$header-height: 48px;

$icon-size: 24px;

$color-white: #fff;

// mixins
@mixin flex-row {
  display: flex;
  flex-direction: row;
}

@mixin icon-default {
  background-image: url("./assets/img/Sprites.svg");
  background-repeat: no-repeat;
  display: inline-block;
}

@mixin icon-bind($position, $size: $icon-size) {
  background-position: $position;
  height: $size;
  width: $size;
}

.flex-row-align-center {
  @include flex-row();
  align-items: center;
}

// style chung
* {
  box-sizing: border-box;
  font-family: inherit;
  font-size: inherit;
}

html,
body {
  margin: 0;
  padding: 0;
  font-size: 13px;
  font-family: NotoSans;
  overflow-y: auto;
  scroll-behavior: smooth;
}

/* width */
::-webkit-scrollbar {
  width: 10px;
  height: 10px;
}

/* Track */
::-webkit-scrollbar-track {
  background: #f1f1f1;
}

/* Handle */
::-webkit-scrollbar-thumb {
  background: #888;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #555;
}

.icon {
  @include icon-default();

  // icon navbar.
  &.icon-navbar-dashboard {
    @include icon-bind(-30px -1628px);
  }

  &.icon-navbar-cash {
    @include icon-bind(-73px -1628px);
  }

  &.icon-navbar-bank {
    @include icon-bind(-117px -1628px);
  }

  &.icon-navbar-pu {
    @include icon-bind(-161px -1628px);
  }

  // icon header.
  &.icon-three-stripes {
    @include icon-bind(-843px -30px);
    margin-left: 10px;
    cursor: pointer;
  }

  &.icon-chevron-down {
    @include icon-bind(-80px -360px, 14px);
    transform: rotate(90deg);
    margin-left: 10px;
  }

  &.icon-notify {
    @include icon-bind(-788px -30px);
    margin-right: 8px;
  }

  &.icon-refresh {
    @include icon-bind(-423px -201px);

    &:hover {
      @include icon-bind(-1097px -88px);
    }
  }

  &.icon-search {
    @include icon-bind(-992px -360px, 16px);
  }

  &.icon-excel {
    @include icon-bind(-704px -200px);

    &:hover {
      @include icon-bind(-704px -256px);
    }
  }

  &.icon-chevron-down-blue {
    @include icon-bind(-896px -359px, 16px);
  }

  &.icon-close {
    @include icon-bind(-144px -144px);
  }

  &.icon-warning-48 {
    @include icon-bind(-592px -456px, 48px);
  }

  &.icon-error {
    @include icon-bind(-746px -456px, 48px);
  }

  &.icon-arrow-dropdown {
    @include icon-bind(-560px -359px, 16px);
  }

  &.icon-calendar {
    @include icon-bind(-128px -306px, 30px);
  }

  &.icon-question {
    @include icon-bind(-826px -456px, 48px);
  }
}

// Navbar
.navbar {
  height: 100vh;
  width: $navbar-width;
  background-color: #393a3d;

  // &.toggle {

  // }

  //   Logo box
  .logo-box {
    @include flex-row();
    padding: 24px;
    height: $header-height;
    align-items: center;

    .toggle {
      @include icon-default();
      @include icon-bind(-424px -86px);
      margin-right: 10px;
      cursor: pointer;
    }

    .logo-branch {
      @include flex-row();
      align-items: center;

      .logo {
        background-image: url("./assets/img/Logo.svg");
        background-repeat: no-repeat;
        background-size: cover;
        height: 30px;
        width: 30px;
        margin-right: 4px;
      }

      .title {
        font-weight: bold;
        color: $color-white;
      }
    }
  }

  //   Navbar content
  &-content {
    .navbar-item {
      @include flex-row();
      align-items: center;
      height: 42px;
      cursor: pointer;

      &:hover {
        background-color: rgba(255, 255, 255, 0.16);
      }

      &-icon {
        padding-left: 14px;
        margin-right: 8px;
      }

      &-text {
        color: $color-white;
      }
    }
  }
}

// Header
.header {
  position: fixed;
  left: $navbar-width;
  top: 0;
  right: 0;
  height: $header-height;
  @include flex-row();
  justify-content: space-between;
  align-items: center;

  &-left {
    @include flex-row();

    .header-branch {
      @include flex-row();
      align-items: center;
      margin-left: 16px;
      cursor: pointer;

      .title {
        font-weight: bold;
      }
    }
  }

  &-right {
    @include flex-row();
    align-items: center;
    margin-right: 30px;
  }
}

// Content
.content {
  position: fixed;
  left: $navbar-width;
  top: $header-height;
  right: 0;
  bottom: 0;
  padding: 0 20px 0 16px;
  background-color: #f4f5f6;
  overflow: scroll;
  box-sizing: content-box;

  // title box
  .title-box {
    @include flex-row();
    align-items: center;
    justify-content: space-between;
    position: sticky;
    top: 0;
    left: 0;
    height: 64px;
    z-index: 10;
    background-color: #f4f5f6;

    .title {
      font-size: 24px;
      font-weight: bold;
    }
  }

  .toolbar-box {
    @include flex-row();
    align-items: center;
    justify-content: flex-end;
    background-color: $color-white;
    padding-left: 16px;
    padding-right: 16px;
    height: 64px;
    position: sticky;
    left: 0;
  }

  .data {
    padding-bottom: 16px;
    background-color: $color-white;
    // min-width: calc(100% + 30px);
    // overflow-x: auto;

    .table-option {
      @include flex-row();
      align-items: center;
      justify-content: center;

      .btn {
        padding-left: 8px;
        padding-right: 8px;
        color: #0075c0;
        font-weight: bold;
      }
    }
  }

  .divider {
    height: 4px;
    background-color: #f4f5f6;
  }

  .pagination {
    @include flex-row();
    height: 42px;
    justify-content: space-between;
    align-items: center;
    background-color: $color-white;
    // margin: 0 30px 0 16px;
    padding: 0 16px;
    position: sticky;
    bottom: 0;
    left: 0;
    width: 100%;
    z-index: 3;

    .pagination-right {
      @include flex-row();

      .pager {
        @include flex-row();
        align-items: center;

        .page {
          padding-left: 8px;
          padding-right: 8px;
          text-decoration: none;
          cursor: pointer;
          color: #000;

          &.active {
            border: 1px solid #ccc;
            padding-left: 4px;
            padding-right: 4px;
            font-weight: bold;
          }

          &.disable {
            cursor: not-allowed;
            color: #ccc;
          }
        }
      }
    }
  }

  .hidden-left {
    position: fixed;
    left: $navbar-width;
    top: $header-height;
    width: 16px;
    bottom: 0;
    background-color: #f4f5f6;
    z-index: 5;
  }

  .hidden-right {
    position: fixed;
    right: 10px;
    top: $header-height;
    width: 20px;
    bottom: 0;
    background-color: #f4f5f6;
    z-index: 5;
  }

  .flex-center {
    height: 100%;
    width: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
    position: absolute;
    top: 0;
    left: 0;
    bottom: 0;
    right: 0;
    z-index: 2000;

    .loader {
      background-image: url("./assets/loading.svg");
      background-position: center;
      background-size: contain;
      background-repeat: no-repeat;
      height: 32px;
      width: 32px;
      transform: rotate(45 16 16);
    }
  }
}

// components.

// button.
.btn {
  height: 40px;
  padding-left: 24px;
  padding-right: 24px;
  line-height: 40px;
  outline: none;
  background-color: transparent;
  cursor: pointer;
  font-weight: bold;
  border: none;
  border-radius: 4px;

  &.btn-primary {
    background-color: #2ca01c;
    color: $color-white;
    font-weight: bold;
    border: 1px solid #ccc;

    &:hover {
      background-color: #34aa24;
    }
  }

  &.btn-secondary {
    background-color: transparent;
    border: 1px solid #ccc;

    &:hover {
      background-color: #f4f5f6;
    }
  }
}

// input
.input {
  height: 32px;
  outline: none;
  border: 1px solid #ccc;
  padding-left: 12px;
  width: 100%;

  &::placeholder {
    font-style: italic;
  }

  &:focus {
    border-color: #2ca01c;
  }

  &.has-icon {
    padding-right: 28px;
  }

  &.has-error {
    border-color: #f20;
  }
}

.con-input {
  position: relative;

  .text-error {
    display: none;
  }

  &:hover .text-error {
    white-space: nowrap;
    display: inline-block;
    position: absolute;
    top: 100%;
    left: 0;
    background-color: #38393d;
    color: $color-white;
    padding: 8px;
    z-index: 10;
  }

  .icon-input {
    position: absolute;
    right: 8px;
    top: 8px;
  }
}

.label-input {
  display: inline-block;
  margin-top: 8px;
  margin-bottom: 4px;
  font-weight: bold;
}

// checkbox
.checkbox {
  display: none;

  & + label {
    background-color: #fafafa;
    border: 1px solid #cacece;
    box-shadow: 0 1px 2px rgba(0, 0, 0, 0.05),
      inset 0px -15px 10px -12px rgba(0, 0, 0, 0.05);
    height: 18px;
    width: 18px;
    border-radius: 3px;
    display: inline-block;
    position: relative;

    &:active {
      box-shadow: 0 1px 2px rgba(0, 0, 0, 0.05),
        inset 0px 1px 3px rgba(0, 0, 0, 0.1);
    }
  }

  &:checked + label {
    border: 1px solid #adb8c0;
    box-shadow: 0 1px 2px rgba(0, 0, 0, 0.05),
      inset 0px -15px 10px -12px rgba(0, 0, 0, 0.05),
      inset 15px 10px -12px rgba(255, 255, 255, 0.1);
    color: #99a1a7;

    &:active {
      box-shadow: 0 1px 2px rgba(0, 0, 0, 0.05),
        inset 0px 1px 3px rgba(0, 0, 0, 0.1);
    }

    &::after {
      @include icon-default();
      @include icon-bind(-1224px -360px, 16px);
      content: "";
      font-size: 14px;
      position: absolute;
      top: 0;
      left: 0;
    }
  }
}

.radio {
  display: none;

  & + label {
    -webkit-appearance: none;
    background-color: #fafafa;
    border: 1px solid #cacece;
    height: 18px;
    width: 18px;
    border-radius: 50px;
    display: inline-block;
    position: relative;

    &:active {
      box-shadow: 0 1px 2px rgba(0, 0, 0, 0.05),
        inset 0px 1px 3px rgba(0, 0, 0, 0.1);
    }
  }

  &:checked {
    & + label {
      border: 1px solid #2ca01c;

      &:after {
        content: " ";
        width: 10px;
        height: 10px;
        border-radius: 50px;
        position: absolute;
        top: 3px;
        background: #2ca01c;
        left: 3px;
      }

      &:active {
        box-shadow: 0 1px 2px rgba(0, 0, 0, 0.05),
          inset 0px 1px 3px rgba(0, 0, 0, 0.1);
      }
    }
  }
}

// table
.table {
  background-color: $color-white;
  min-width: 100%;
  white-space: nowrap;
  outline: none;
  border-spacing: 0;
  // border-collapse: collapse;

  th {
    border-right: 1px solid #ccc;
    border-bottom: 1px solid #ccc;
    text-align: left;
    padding: 0 16px;
  }

  td {
    border-right: 1px dotted #ccc;
    border-bottom: 1px solid #ccc;
    text-align: left;
    padding: 0 16px;
  }

  thead {
    tr {
      height: 32px;
      th {
        background-color: #eceef1;
      }
    }
  }

  tbody {
    tr {
      height: 48px;
    }
  }

  .view-white {
    background-color: $color-white;
    border: none !important;
    width: 16px !important;
    min-width: 16px !important;
    max-width: 16px !important;
    padding: 0;
  }

  .pin {
    position: sticky;
    background-color: $color-white;
    z-index: 2;
  }
}

// dialog
.dialog {
  position: fixed;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  display: flex;
  justify-content: center;
  align-items: center;
  .dialog-background {
    position: absolute;
    height: 100%;
    width: 100%;
    background-color: rgba(0, 0, 0, 0.4);
  }

  .dialog-content {
    z-index: 100;
    background-color: $color-white;
    margin: 20px auto;
    width: 900px;
    position: relative;

    .tooltip-box {
      display: inline-block;
      position: absolute;
      right: 0;
      top: 0;

      .btn-close-dialog {
        height: 40px;
        width: 40px;
        display: flex;
        justify-content: center;
        align-items: center;
        cursor: pointer;
      }
    }

    .dialog-header {
      @include flex-row();
      align-items: center;
      height: 52px;
      padding-left: 32px;
      padding-right: 32px;

      .dialog-title {
        font-size: 24px;
        font-weight: bold;
      }

      .option {
        @include flex-row();
        align-items: center;
        padding-left: 16px;
        padding-right: 16px;
      }
    }

    .dialog-body {
      padding-left: 32px;
      padding-right: 32px;
      padding-bottom: 32px;
    }

    .dialog-footer {
      @include flex-row();
      justify-content: space-between;
      align-items: center;
      height: 72px;
      margin-left: 32px;
      margin-right: 32px;
      border-top: 1px solid #ccc;
    }
  }

  &.hide {
    display: none;
  }
}

// dropdown
.dropdown {
  position: relative;

  .dropdown-btn {
  }

  .dropdown-content {
    min-width: 100%;
    position: absolute;
    background-color: $color-white;
    border: 1px solid #ccc;
    white-space: nowrap;
    z-index: 20;
    min-width: 100%☺;

    &.reserve {
      top: auto;
      bottom: 100%;
    }

    &.hide {
      display: none;
    }

    &.right {
      right: 0;
    }

    .dropdown-item {
      height: 32px;
      line-height: 32px;
      padding-left: 12px;
      padding-right: 12px;

      &:hover {
        background-color: #f4f5f6;
        color: #2ca01c;
        cursor: pointer;
      }
    }
  }
}

// autocomplete
.autocomplete {
  .icon-dropdown-box {
    cursor: pointer;
    height: 30px;
    width: 30px;
    top: 1px;
    right: 1px;
    display: flex;
    align-items: center;
    justify-content: center;

    &:hover {
      background-color: #f4f5f6;
      border-left: none;
      border-radius: 4px;
    }
  }

  .active {
    background-color: #2ca01c;
    color: $color-white;
  }
}

// tooltip
.tooltip {
  position: relative;

  &-text {
    display: none;
    white-space: nowrap;
    position: absolute;
    top: 100%;
    left: 0;
    background-color: #38393d;
    color: $color-white;
    padding: 8px;
    z-index: 10;
  }

  &:hover &-text {
    display: inline-block;
  }
}

// other
.table-option-dropdown {
  position: fixed;
  background-color: $color-white;
  z-index: 100;
  white-space: nowrap;
  border: 1px solid #ccc;

  .dropdown-item {
    height: 32px;
    line-height: 32px;
    padding-left: 12px;
    padding-right: 12px;

    &:hover {
      background-color: #f4f5f6;
      color: #2ca01c;
      cursor: pointer;
    }
  }
}

// grid view
.col-1 {
  width: 8.33%;
  flex: none;
}
.col-2 {
  width: 16.66%;
  flex: none;
}
.col-3 {
  width: 25%;
  flex: none;
}
.col-4 {
  width: 33.33%;
  flex: none;
}
.col-5 {
  width: 41.66%;
  flex: none;
}
.col-6 {
  width: 50%;
  flex: none;
}
.col-7 {
  width: 58.33%;
  flex: none;
}
.col-8 {
  width: 66.66%;
  flex: none;
}
.col-9 {
  width: 75%;
  flex: none;
}
.col-10 {
  width: 83.33%;
  flex: none;
}
.col-11 {
  width: 91.66%;
  flex: none;
}
.col-12 {
  width: 100%;
  flex: none;
}

.row {
  @include flex-row();
  flex-wrap: wrap;
}
</style>