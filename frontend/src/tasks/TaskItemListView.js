import ErrorMessage from '../ErrorMessage';
import TaskItem from "./TaskItem";

export default function TaskItemListView({taskItems, error, loading, handleAddBtnOnClick,
                                          handleTaskItemEdit, handleTaskItemDelete}) {
  return (
    <section className="block container">
      {/* Add button */}
      <button className="button mb-3" onClick={handleAddBtnOnClick}>
        Add Task
      </button>

      {/* Global error message */}
      <ErrorMessage message={error} />

      {/* Loading message/spinner */}
      <div style={{display: loading ? null : 'none'}}>
        <i className='fa-solid fa-spinner fa-spin mr-2' />
        <span>Loading your tasks...</span>
      </div>

      {/* Task list */}
      <ul>
        {taskItems.map(x => (
          <li key={x.id}>
            <TaskItem taskItem={x} onEdit={handleTaskItemEdit} onDelete={handleTaskItemDelete} />
          </li>
        ))}
      </ul>
    </section>
  );
}
