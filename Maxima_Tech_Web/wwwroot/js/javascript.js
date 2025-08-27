function formatNumero(input) {
    input.value = input.value.replace(/[^0-9.,]/g, '').replace('.', ',');
    if (input.value.trim() === '') {
        input.value = '';
    } else if (!isNaN(input.value.replace(',', '.'))) {
        input.value = parseFloat(input.value.replace(',', '.')).toFixed(2).replace('.', ',');
    }
}