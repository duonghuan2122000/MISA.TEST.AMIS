<template>
  <div
    class="navbar"
    :class="{ toggle: isNavbarToggle, slideInRight: !isNavbarToggle }"
  >
    <!-- Logo box -->
    <div class="logo-box" v-if="!isNavbarToggle">
      <div class="toggle"></div>
      <div class="logo-branch">
        <div class="logo"></div>
        <div class="title">KẾ TOÁN</div>
      </div>
    </div>
    <!-- Logo box -->

    <div
      class="logo-box-toggle"
      v-else
      @click.prevent="$emit('update:isNavbarToggle')"
    >
      <div class="icon icon-navbar-three-stripes"></div>
    </div>

    <div
      class="navbar-content"
      style="overflow: auto; height: calc(100vh - 48px)"
    >
      <div class="navbar-item">
        <div class="navbar-item-icon">
          <div class="icon icon-navbar-dashboard"></div>
        </div>
        <div class="navbar-item-text">Tổng quan</div>
      </div>

      <!-- Add class để active link -->
      <router-link :to="{ name: 'Employee' }" class="navbar-item">
        <div class="navbar-item-icon">
          <div class="icon icon-navbar-cash"></div>
        </div>
        <div class="navbar-item-text">Tiền mặt</div>
      </router-link>

      <div class="navbar-item">
        <div class="navbar-item-icon">
          <div class="icon icon-navbar-bank"></div>
        </div>
        <div class="navbar-item-text">Tiền gửi</div>
      </div>

      <div class="navbar-item">
        <div class="navbar-item-icon">
          <div class="icon icon-navbar-pu"></div>
        </div>
        <div class="navbar-item-text">Mua hàng</div>
      </div>
      <div class="navbar-item">
        <div class="navbar-item-icon">
          <div class="icon icon-navbar-pu"></div>
        </div>
        <div class="navbar-item-text">Mua hàng</div>
      </div>
      <div class="navbar-item">
        <div class="navbar-item-icon">
          <div class="icon icon-navbar-pu"></div>
        </div>
        <div class="navbar-item-text">Mua hàng</div>
      </div>
      <div class="navbar-item">
        <div class="navbar-item-icon">
          <div class="icon icon-navbar-pu"></div>
        </div>
        <div class="navbar-item-text">Mua hàng</div>
      </div>
      <div class="navbar-item">
        <div class="navbar-item-icon">
          <div class="icon icon-navbar-pu"></div>
        </div>
        <div class="navbar-item-text">Mua hàng</div>
      </div>
      <div class="navbar-item">
        <div class="navbar-item-icon">
          <div class="icon icon-navbar-pu"></div>
        </div>
        <div class="navbar-item-text">Mua hàng</div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    isNavbarToggle: {
      type: Boolean,
      default: false,
    },
  },
};
</script>