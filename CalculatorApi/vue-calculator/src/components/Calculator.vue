<template>
  <div>
    <h1>Calculator</h1>
    <input type="number" v-model="firstNumber" placeholder="First Number" />
    <input type="number" v-model="secondNumber" placeholder="Second Number" />
    <select v-model="operation">
      <option value="+">+</option>
      <option value="-">-</option>
      <option value="*">*</option>
      <option value="/">/</option>
    </select>
    <button @click="calculate">Calculate</button>
    <p v-if="result !== null">Result: {{ result }}</p>
    <p v-if="error">{{ error }}</p>
  </div>
</template>

<script lang="ts">
import { ref } from 'vue';
import apiClient from '../api/axios'; // Импортируйте созданный клиент
export default {
  name: 'CalculatorComponent',
  setup() {
    const firstNumber = ref<number | null>(null);
    const secondNumber = ref<number | null>(null);
    const operation = ref<string>('');
    const result = ref<number | null>(null);
    const validationErrors = ref<Record<string, string>>({});
    const error = ref<string | null>(null);

    interface ValidationErrorResponse {
      errors: {
        [key: string]: string[]; // Ошибки привязаны к названиям свойств модели
      };
    }
    const calculate = async () => {
      error.value = null;
      try {
        validationErrors.value = {};
        const response = await apiClient.post('/calculate', {
          firstNumber: firstNumber.value,
          secondNumber: secondNumber.value,
          operation: operation.value,
        });
        result.value = response.data.result;
      }catch (error: any) {
        if (error.response.status === 400) {
          // Обработка ошибок валидации
          const parsedErrors = parseValidationErrors(error.response.data as ValidationErrorResponse);
          error.value = parsedErrors;
        } else {
          // Обработка других ошибок
          error.value = 'Something went wrong. Please try again.';
        }
      /*catch (err: any) {
        error.value = err.response?.data?.secondNumber || 'An error occurred';*/
      }
    };

    function parseValidationErrors(errorResponse: ValidationErrorResponse): Record<string, string> {
      const errors: Record<string, string> = {};

      for (const [field, messages] of Object.entries(errorResponse.errors)) {
        errors[field] = messages.join(', '); // Объединяем несколько сообщений
      }

      return errors;
    }

    return {
      firstNumber,
      secondNumber,
      operation,
      result,
      validationErrors,
      error,
      calculate,
    };
  },
};
</script>



<!--
<template>
  <div class="calculator">
    <input
        type="text"
        v-model="display"
        class="calculator-display"
        disabled
    />
    <div class="calculator-buttons">
      <button v-for="button in buttons" :key="button" @click="onButtonClick(button)">
        {{ button }}
      </button>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import axios from 'axios';

export default defineComponent({
  name: 'Calculator',
  setup() {
    const display = ref<string>(''); // Отображаемый текст на экране калькулятора
    const isLoading = ref<boolean>(false); // Состояние загрузки
    const buttons = [
      '7', '8', '9', '/',
      '4', '5', '6', '*',
      '1', '2', '3', '-',
      '0', '.', '=', '+',
      'C'
    ]; // Кнопки калькулятора

    const onButtonClick = async (button: string) => {
      if (button === 'C') {
        display.value = ''; // Очистка экрана
      } else if (button === '=') {
        if (display.value.trim() === '') {
          display.value = 'Error';
          return;
        }
        isLoading.value = true; // Устанавливаем состояние загрузки
        try {
          const response = await axios.post('https://localhost:7152', {
            expression: display.value,
          });
          display.value = response.data.result.toString(); // Устанавливаем результат
        } catch (error) {
          console.error(error);
          display.value = 'Error';
        } finally {
          isLoading.value = false; // Сбрасываем состояние загрузки
        }
      } else {
        display.value += button; // Добавление символов к выражению
      }
    };

    return {
      display,
      buttons,
      onButtonClick,
      isLoading,
    };
  }
});
</script>

<style scoped>
.calculator {
  max-width: 400px;
  margin: 50px auto;
  padding: 20px;
  border: 2px solid #ccc;
  border-radius: 10px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
  text-align: center;
  background-color: #f9f9f9;
}

.calculator-display {
  width: 100%;
  height: 50px;
  margin-bottom: 10px;
  font-size: 1.5em;
  text-align: right;
  padding: 5px;
  border: 1px solid #ddd;
  border-radius: 5px;
  background-color: #fff;
}

.calculator-buttons {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 10px;
}

button {
  height: 50px;
  font-size: 1.2em;
  border: none;
  border-radius: 5px;
  background-color: #007BFF;
  color: white;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

button:hover {
  background-color: #0056b3;
}
</style>

-->


<!--
<template>
  <div>
    <h1>Calculator</h1>
    <div>
      <input type="number" v-model="firstNumber" placeholder="First Number" />
      <input type="number" v-model="secondNumber" placeholder="Second Number" />
      <select v-model="operation">
        <option value="+">+</option>
        <option value="-">-</option>
        <option value="*">*</option>
        <option value="/">/</option>
      </select>
      <button @click="calculate">Calculate</button>
    </div>
    <p v-if="result !== null">Result: {{ result }}</p>
    <p v-if="error">{{ error }}</p>
  </div>
</template>

<script lang="ts">
import { ref } from 'vue';

export default {
  name: 'CalculatorComponent',
  setup() {
    const firstNumber = ref<number | null>(null);
    const secondNumber = ref<number | null>(null);
    const operation = ref<string>('');
    const result = ref<number | null>(null);
    const error = ref<string | null>(null);

    const calculate = () => {
      if (firstNumber.value === null || secondNumber.value === null || !operation.value) {
        error.value = 'Please provide valid inputs';
        return;
      }

      try {
        switch (operation.value) {
          case '+':
            result.value = firstNumber.value + secondNumber.value;
            break;
          case '-':
            result.value = firstNumber.value - secondNumber.value;
            break;
          case '*':
            result.value = firstNumber.value * secondNumber.value;
            break;
          case '/':
            if (secondNumber.value === 0) throw new Error('Division by zero');
            result.value = firstNumber.value / secondNumber.value;
            break;
          default:
            throw new Error('Invalid operation');
        }
        error.value = null;
      } catch (err: any) {
        error.value = err.message;
      }
    };

    return {
      firstNumber,
      secondNumber,
      operation,
      result,
      error,
      calculate,
    };
  },
};
</script>

<style scoped>
/* Добавьте стили по необходимости */
</style>
-->
