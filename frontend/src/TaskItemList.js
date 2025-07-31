import TaskItem from "./TaskItem";

export default function TaskItemList({items}) {
  return (
    <section className="block container">
      <button className="button mb-3">Add Task</button>
      <ul>
        {items.map(x => (
          <li key={x.id}>
            <TaskItem taskItem={x} />
          </li>
        ))}
      </ul>
    </section>
  );
}
