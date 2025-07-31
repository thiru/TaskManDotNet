import Header from './Header.js';
import TaskItemList from './TaskItemList.js';

const sampleItems = [
  {id: 1, description: 'first', isDone: false},
  {id: 2, description: 'second', isDone: true}
];

function App() {
  return (
    <div>
      <Header />
      <TaskItemList items={sampleItems} />
    </div>
  );
}

export default App;
