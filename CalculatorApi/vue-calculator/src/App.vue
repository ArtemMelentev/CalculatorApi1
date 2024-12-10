<template>
  <div id="app">
    <h1>Vue 3 Calculator</h1>
    <Calculator />
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import Calculator from './components/Calculator.vue';

export default defineComponent({
  name: 'App',
  components: {
    Calculator,
  },
});
</script>

<style>
#app {
  font-family: Arial, sans-serif;
  text-align: center;
  color: #333;
}
</style>
