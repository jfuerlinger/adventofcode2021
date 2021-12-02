const fs = require('fs')

fs.readFile('./input.txt', 'utf8', (err, data) => {
  if (err) {
    console.error(err);
    return;
  }

  const movements = data
    .split('\n');

  console.log(`puzzle's solution is ${processMovements(movements)}`);

});

processMovements = (movements) => {
  let posHorizontal = 0;
  let posDepth = 0;
  for (let i = 0; i < movements.length; i++) {
    let movement = movements[i].split(' ');
    let direction = movement[0];
    let amount = parseInt(movement[1]);

    switch (direction) {
      case 'forward':
        posHorizontal += amount;
        break;

      case 'down':
        posDepth += amount;
        break;

      case 'up':
        posDepth -= amount;
        break;
    }
  }

  return posHorizontal * posDepth;
}

