const fs = require('fs')

fs.readFile('./input.txt', 'utf8', (err, data) => {
  if (err) {
    console.error(err);
    return;
  }

  const numbers = data
    .split('\n')
    .filter(str => !isNaN(str))
    .map(x => parseInt(x));

  let nrOfIncreases = 0;
  for (let i = 1; i < numbers.length; i++) {
    if (numbers[i - 1] < numbers[i]) {
      nrOfIncreases++;
    }
  }

  console.log(`Number of depth measurement increases: ${nrOfIncreases}`);
})
