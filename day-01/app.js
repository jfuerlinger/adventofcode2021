const fs = require('fs')

fs.readFile('./input.txt', 'utf8', (err, data) => {
  if (err) {
    console.error(err);
    return;
  }

  const measurements = data
    .split('\n')
    .filter(str => !isNaN(str))
    .map(x => parseInt(x));



  console.log(`Number of depth measurement increases (simple): ${countIncreases(measurements)}`);
  console.log(`Number of depth measurement increases (sliding window): ${countIncreasesSlidingWindow(measurements)}`);
});

countIncreases = (measurements) => {
  let nrOfIncreases = 0;
  for (let i = 1; i < measurements.length; i++) {
    if (measurements[i - 1] < measurements[i]) {
      nrOfIncreases++;
    }
  }

  return nrOfIncreases;
}

countIncreasesSlidingWindow = (measurements) => {
  let nrOfIncreases = 0;
  const windowSums = [];

  for (let i = 1; i < measurements.length-1; i++) {
    windowSums.push(measurements[i-1] + measurements[i] + measurements[i+1])
  }

  return countIncreases(windowSums);
}
