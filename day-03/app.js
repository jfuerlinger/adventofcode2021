const fs = require('fs')

fs.readFile('./input.txt', 'utf8', (err, data) => {
  if (err) {
    console.error(err);
    return;
  }

  const numbers = data
    .split('\n')
    .filter(n => n.length > 0);

  console.log(`puzzle's solution (part 1) is ${analyseNumbers(numbers)}`);
  console.log(`puzzle's solution (part 2) is ${analyseSupportRating(numbers)}`);
});

analyseNumbers = (numbers) => {

  occurencesDigitZero = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
  occurencesDigitOne = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];


  for (let i = 0; i < numbers.length; i++) {
    for (let j = 0; j < numbers[i].length; j++) {
      if (numbers[i][j] == '0') {
        occurencesDigitZero[j]++;
      } else {
        occurencesDigitOne[j]++;
      }
    }
  }

  console.log(occurencesDigitZero);
  console.log(occurencesDigitOne);

  gammaBinary = [];
  for (let i = 0; i < occurencesDigitZero.length; i++) {
    if (occurencesDigitZero[i] > occurencesDigitOne[i]) {
      gammaBinary[i] = 0;
    } else {
      gammaBinary[i] = 1;
    }
  }

  console.log(`gamma=${gammaBinary} => ${binToDecimal(gammaBinary)}`); // 190


  epsilonBinary = [];
  for (let i = 0; i < occurencesDigitZero.length; i++) {
    if (occurencesDigitZero[i] < occurencesDigitOne[i]) {
      epsilonBinary[i] = 0;
    } else {
      epsilonBinary[i] = 1;
    }
  }

  console.log(`gamma=${epsilonBinary} => ${binToDecimal(epsilonBinary)}`);  // 3905

  return binToDecimal(gammaBinary) * binToDecimal(epsilonBinary);
}

binToDecimal = (binaryNumber) => {
  let result = 0;
  let pow = 0;
  for (let i = binaryNumber.length - 1; i >= 0; i--) {
    result += parseInt(binaryNumber[i]) * Math.pow(2, pow++);
  }

  return result;
}

filterByOccurance = (numbers, index, byHighestOccurance) => {

  if (index >= numbers[0].length) {
    return;
  }

  filterCriteria = getOccuranceAtIndex(numbers, index, byHighestOccurance);
  numbers = numbers.filter(n => n[index] == filterCriteria);

  if (numbers.length == 1) {
    return numbers[0];
  } else {
    return filterByOccurance(numbers, index + 1, byHighestOccurance);
  }
}

getOccuranceAtIndex = (numbers, index, countHighestOccurance) => {
  cntDigitZero = 0;
  cntDigitOne = 0;
  for (let i = 0; i < numbers.length; i++) {
    if (numbers[i][index] == '0') {
      cntDigitZero++;
    } else {
      cntDigitOne++;
    }
  }

  if (countHighestOccurance) {
    if (cntDigitOne > cntDigitZero) {
      return 1;
    } else if (cntDigitOne == cntDigitZero) {
      return 1;
    } else {
      return 0;
    }
  } else {
    if (cntDigitOne < cntDigitZero) {
      return 1;
    } else if (cntDigitOne == cntDigitZero) {
      return 0;
    } else {
      return 0;
    }
  }
}


analyseSupportRating = (numbers) => {

  let oxygen = binToDecimal(filterByOccurance(numbers, 0, true));
  let co2 = binToDecimal(filterByOccurance(numbers, 0, false));
  console.log(`oxygen generator rating=${oxygen}`);
  console.log(`CO2 scrubber rating=${co2}`);

  return oxygen * co2;
}



