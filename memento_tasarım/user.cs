public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string IDNo { get; set; }

        public UserMemento Backup()
        {
            return new UserMemento
            {
                UserId = this.UserId,
                Name = this.Name,
                IDNo = this.IDNo
            };
        }

        public void Restore(UserMemento memento)
        {
            this.UserId = memento.UserId;
            this.Name = memento.Name;
            this.IDNo = memento.IDNo;
        }

        public override string ToString()
        {
            return String.Format("User ID: {0} Name: {1} ID NO: {2}\n", UserId, Name, IDNo);
        }
    }