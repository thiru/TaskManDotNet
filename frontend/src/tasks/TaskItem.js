export default function TaskItem({taskItem, onEdit, onDelete}) {
  return (
    <span className="has-hover-show">
      <label className="checkbox">
        <input checked={taskItem.isDone}
               onChange={() => onEdit({...taskItem, isDone: !taskItem.isDone})}
               type="checkbox" />
        <span className="ml-1">{taskItem.description}</span>
      </label>
      <button className="button is-white is-small ml-1 mb-2 is-invisible hover-show"
              onClick={() => {
                const newDescription = window.prompt('New Description:', taskItem.description);
                if (newDescription !== null) {
                  onEdit({...taskItem, description: newDescription});
                }
              }}
              title="Edit description">
        <span className="icon">
          <i className="fa-solid fa-pen-to-square"></i>
        </span>
      </button>
      <button className="button is-warning is-light is-small ml-1 mb-2 is-invisible hover-show"
              onClick={() => {
                if (window.confirm('Are you sure you would like to permanently delete the following task: \n\n' + taskItem.description))
                  onDelete(taskItem.id)
              }}
              title="Permanently delete task">
        <span className="icon">
          <i className="fa-solid fa-circle-minus"></i>
        </span>
      </button>
    </span>
  );
}
