<template>
  <div class="dropdown autocomplete">
    <div class="dropdown-btn con-input">
      <input
        type="text"
        class="input has-icon"
        :value="valueInput"
        v-bind="inputAttributes"
        @focus="showSuggestion"
        @blur="onBlur"
        @keydown.up.prevent="up"
        @keydown.down.prevent="down"
        @keydown.enter.prevent="enter"
        @input="onInput"
      />
      <div
        class="icon-input icon-dropdown-box"
        @mousedown.prevent="toggleSuggestion"
      >
        <div class="icon icon-arrow-dropdown"></div>
      </div>
    </div>
    <div class="dropdown-content" :class="{ hide: !isShow }">
      <div
        v-for="(suggestion, i) in suggestionData"
        :key="i"
        class="dropdown-item"
        :class="{ active: current == i }"
        @click.prevent="clickSuggestion(suggestion, i)"
      >
        {{ suggestion.text }}
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    /**
     * Danh sách suggestion của autocomplete.
     * CreatedBy: dbhuan 17/05/2021
     */
    suggestions: {
      type: Array,
      required: true,
    },

    /**
     * Giá trị khởi tạo cho input
     * CreatedBy: dbhuan 17/05/2021
     */
    value: {
      type: String,
      default: null,
    },

    /**
     * Attribute thêm cho input
     * CreatedBy: dbhuan 18/05/2021
     */
    inputAttributes: {
      type: Object,
      default: null,
    },
  },
  data: () => ({
    /**
     * Xác định trạng thái popup autocomplete
     * CreatedBy: dbhuan 17/05/2021
     */
    isShow: false,

    /**
     * vị trí hiện tại
     * CreatedBy: dbhuan 17/05/2021
     */
    current: 0,

    /**
     * Danh sách suggesstion của autocomplete có lọc
     * CreatedBy: dbhuan 17/05/2021
     */
    suggestionData: [],

    /**
     * Giá trị của input
     * CreatedBy: dbhuan 17/05/2021
     */
    valueInput: "",
  }),
  methods: {
    /**
     * Đảo ngược trạng thái popup
     * CreatedBy: dbhuan 17/05/2021
     */
    toggleSuggestion() {
      if (this.isShow) {
        this.isShow = false;
      } else {
        this.showSuggestion();
      }
    },

    /**
     * Hiển thị popup
     * CreatedBy: dbhuan 17/05/2021
     */
    showSuggestion() {
      this.$el.querySelector("input").focus();
      this.isShow = true;
    },

    /**
     * Nhấn enter
     * CreatedBy: dbhuan 17/05/2021
     */
    enter() {
      this.$emit("update:value", this.suggestionData[this.current].value);
      this.valueInput = this.suggestionData[this.current].text;
      this.isShow = false;
    },

    /**
     * Nhấn up
     * CreatedBy: dbhuan 17/05/2021
     */
    up() {
      if (this.current > 0) this.current--;
      this.valueInput = this.suggestionData[this.current].text;
    },

    /**
     * Nhấn down
     * CreatedBy: dbhuan 17/05/2021
     */
    down() {
      if (this.current < this.suggestions.length - 1) this.current++;
      this.valueInput = this.suggestionData[this.current].text;
    },

    /**
     * Chọn một suggesstion
     * CreatedBy: dbhuan 17/05/2021
     */
    clickSuggestion(suggestion, index) {
      this.current = index;
      this.isShow = false;
      this.$emit("update:value", suggestion.value);
      this.valueInput = suggestion.text;
    },

    /**
     * Phương thức close dropdown.
     * CreatedBy: dbhuan (09/05/2021)
     */
    close(e) {
      if (!this.$el.contains(e.target)) {
        this.isShow = false;
      }
    },

    /**
     * Blur input
     * CreatedBy: dbhuan 17/05/2021
     */
    onBlur() {
      setTimeout(() => {
        this.isShow = false;
        this.$emit('blur')
      }, 500);
    },

    /**
     * Nhập vào input
     * CreatedBy: dbhuan 17/05/2021
     */
    onInput(e) {
      let val = e.target.value;
      this.valueInput = val;
      this.current = 0;
      if (this.suggestions) {
        this.suggestionData = this.suggestions.filter((s) =>
          s.text.toLowerCase().includes(val.toLowerCase())
        );
        this.isShow = true;
      }
    },
  },

  watch: {
    suggestions: function () {
      this.suggestionData = this.suggestions;
      let index = this.suggestionData.findIndex((s) => s.value == this.value);
      if (index >= 0) {
        this.current = index;
        this.valueInput = this.suggestionData[this.current].text;
      } else {
        this.current = 0;
        this.valueInput = "";
      }
    },
  },

  mounted() {
    document.addEventListener("click", this.close);
  },

  beforeDestroy() {
    document.removeEventListener("click", this.close);
  },
};
</script>